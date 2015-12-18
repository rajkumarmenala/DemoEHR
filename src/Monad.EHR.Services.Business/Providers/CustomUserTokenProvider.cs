using Microsoft.AspNet.DataProtection;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.OptionsModel;
using Monad.EHR.Domain.Entities.Identity;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Monad.EHR.Services.Business
{
    public interface ICustomUserTokenProvider
    {
        Task<User> GetUserFromToken(string token, UserManager<User> manager);
    }
    public class CustomUserTokenProvider : ICustomUserTokenProvider, IUserTokenProvider<User>
    {
        public CustomUserTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<DataProtectionTokenProviderOptions> options)
        {
            if (dataProtectionProvider == null)
            {
                throw new ArgumentNullException(nameof(dataProtectionProvider));
            }
            Options = options?.Value ?? new DataProtectionTokenProviderOptions();
            // Use the Name as the purpose which should usually be distinct from others
            Protector = dataProtectionProvider.CreateProtector(Name ?? "CustomUserTokenProvider");
        }
        protected IDataProtector Protector { get; private set; }
        protected DataProtectionTokenProviderOptions Options { get; private set; }
        public string Name { get { return Options.Name; } }

        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<User> manager, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateAsync(string purpose, UserManager<User> manager, User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var ms = new MemoryStream();
            var userId = await manager.GetUserIdAsync(user);
            using (var writer = ms.CreateWriter())
            {
                writer.Write(DateTimeOffset.UtcNow);
                writer.Write(userId);
                writer.Write(user.UserName);
                writer.Write(purpose ?? "");
                string stamp = null;
                if (manager.SupportsUserSecurityStamp)
                {
                    stamp = await manager.GetSecurityStampAsync(user);
                }
                writer.Write(stamp ?? "");
            }
            var protectedBytes = Protector.Protect(ms.ToArray());
            return Convert.ToBase64String(protectedBytes);
        }

        public virtual async Task<bool> ValidateAsync(string purpose, string token, UserManager<User> manager, User user)
        {
            try
            {
                var unprotectedData = Protector.Unprotect(Convert.FromBase64String(token));
                var ms = new MemoryStream(unprotectedData);
                using (var reader = ms.CreateReader())
                {
                    var creationTime = reader.ReadDateTimeOffset();
                    var expirationTime = creationTime + Options.TokenLifespan;
                    if (expirationTime < DateTimeOffset.UtcNow)
                    {
                        return false;
                    }

                    var userId = reader.ReadString();
                    var actualUserId = await manager.GetUserIdAsync(user);
                    if (userId != actualUserId)
                    {
                        return false;
                    }
                    var userName = reader.ReadString();
                    var purp = reader.ReadString();
                    if (!string.Equals(purp, purpose))
                    {
                        return false;
                    }
                    var stamp = reader.ReadString();
                    if (reader.PeekChar() != -1)
                    {
                        return false;
                    }

                    if (manager.SupportsUserSecurityStamp)
                    {
                        return stamp == await manager.GetSecurityStampAsync(user);
                    }
                    return stamp == "";
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch
            {
                // Do not leak exception
            }
            return false;
        }

        public async Task<User> GetUserFromToken(string token, UserManager<User> manager)
        {
            try
            {
                var unprotectedData = Protector.Unprotect(Convert.FromBase64String(token));
                var ms = new MemoryStream(unprotectedData);
                using (var reader = ms.CreateReader())
                {
                    var creationTime = reader.ReadDateTimeOffset();
                    var expirationTime = creationTime + Options.TokenLifespan;
                    if (expirationTime < DateTimeOffset.UtcNow)
                    {
                        return null;
                    }

                    var userId = reader.ReadString();
                    var userName = reader.ReadString();
                    var purp = reader.ReadString();
                    var stamp = reader.ReadString();

                    var user = await  manager.FindByIdAsync(userId);
                    var actualUserId = await  manager.GetUserIdAsync(user);
                    if (userId != actualUserId)
                    {
                        return null;
                    }

                    if (manager.SupportsUserSecurityStamp)
                    {
                        return (stamp == await manager.GetSecurityStampAsync(user)) ? user: null;
                    }
                    return (stamp == "") ? user : null; ;
                }
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    internal static class StreamExtensions
    {
        internal static readonly Encoding DefaultEncoding = new UTF8Encoding(false, true);

        public static BinaryReader CreateReader(this Stream stream)
        {
            return new BinaryReader(stream, DefaultEncoding, true);
        }

        public static BinaryWriter CreateWriter(this Stream stream)
        {
            return new BinaryWriter(stream, DefaultEncoding, true);
        }

        public static DateTimeOffset ReadDateTimeOffset(this BinaryReader reader)
        {
            return new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);
        }

        public static void Write(this BinaryWriter writer, DateTimeOffset value)
        {
            writer.Write(value.UtcTicks);
        }
    }
}
