using Microsoft.AspNet.Authentication;
using Monad.EHR.Web.App.Security;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Monad.EHR.Web.App.Policies
{
    public class TokenAuthOptions : AuthenticationOptions
    {
        public const string Scheme = "Bearer";
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        public TokenAuthOptions()
        {
            AuthenticationScheme = Scheme;
            AutomaticAuthenticate = true;
        }

        public static TokenAuthOptions GetInstance(RsaSecurityKey key)
        {
          
            var tokenOptions = new TokenAuthOptions()
            {
                Audience = "ExampleAudience",
                Issuer = "ExampleIssuer",
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature)
            };
            return tokenOptions;
        }
    }
}
