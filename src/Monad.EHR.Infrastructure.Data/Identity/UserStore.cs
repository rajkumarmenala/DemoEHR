using Monad.EHR.Domain.Entities;
using Monad.EHR.Domain.Entities.Identity;
using Monad.EHR.Domain.Interfaces.Identity;
using Monad.EHR.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Threading;

namespace WesternUnion.Speedpay.Admin.Infrastructure.Data.Identity
{
    /// <summary>
    ///     Admin user store.
    /// </summary>
    public class UserStore :  IIdentityRepository
    {
        #region error messages

        private const string _emsg_ConnectioIsRequired = "Dbconnection is required!";
        private const string _emsg_UserIsRequired = "User is required!";
        private const string _emsg_UserNameIsRequired = "User name is required!";
        private const string _emsg_UserIdIsRequired = "User Id is required!";
        private const string _emsg_LoginIsRequired = "Login is required!";

        #endregion error messages

        /// <summary>
        /// Adds the claim asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="claim">The claim.</param>
        /// <returns></returns>
        public Task AddClaimAsync(User user, Claim claim)
        {
            return Task.FromResult<int>(0);
        }

        /// <summary>
        /// Gets the claims asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task<IList<Claim>> GetClaimsAsync(User user)
        {
            return Task.FromResult<IList<Claim>>(new List<Claim>());
        }

        public Task RemoveClaimAsync(User user, Claim claim)
        {
            return Task.FromResult<int>(0);
        }

        //fields
        private DbConnection _connection;

        /// <summary>
        /// Database connection for User Store
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public DbConnection Connection
        {
            get { return _connection; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(_emsg_ConnectioIsRequired);

                _connection = value;
            }
        }

     
        //public UserStore(ConnectionStringSettingsCollection connectionStrings)
        //    : base(connectionStrings)
        //{
        //    _connection = this._connectionStrings[ConnectionStringNames.MDM].GetClosedConnection();
        //}

        #region IDisposable implementation

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
            _connection = null;
        }

        #endregion IDisposable implementation

        #region IUserStore implementation

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(_emsg_UserIsRequired);

            return Task.Factory.StartNew(() =>
            {
                DataTable userDataTable = new DataTable();

                //GetUserUdt(user, ref userDataTable);
                //var userUdt = userDataTable.AsTableValuedParameter("users_udtt");
                //if (_connection.State != ConnectionState.Open)
                //    _connection.Open();
                //var p = new DynamicParameters();
                //p.Add("users_udtt", userUdt);
                //p.Add("activity_id", DBNull.Value, dbType: DbType.String, direction: ParameterDirection.Input);
                //p.Add("role_id", DBNull.Value, dbType: DbType.Int32, direction: ParameterDirection.Input);
                //p.Add("user_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //_connection.Execute("USP_UPSERT_USERS_ROLE_ACTIVITY", p, commandType: CommandType.StoredProcedure);
                string newUserId = "";// p.Get<int>("user_id").ToString();
                _connection.Close();
                return newUserId;
            });
        }


        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(_emsg_UserIsRequired);

            return Task.Factory.StartNew(() =>
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                //_connection.Execute(@"update Users set USER_NAME = @userName,
                //                        PASSWORD_HASH = @passwordHash ,
                //                        SECURITY_STAMP= @SecurityStamp,
                //                        PASSWORD_RESET_TOKEN = @PasswordResetToken,
                //                        PASSWORD_RESET_TOKEN_GENERATED_TIME =  @PasswordResetTokenGeneratedTime
                //                        where USER_ID = @UserId",
                //    user);
            });
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public Task DeleteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(_emsg_UserIsRequired);

            return Task.Factory.StartNew(() =>
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                //_connection.Execute("delete from Users where USER_ID = @USER_ID", new { user.UserId });
            });
        }

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public Task<User> FindByIdAsync(string userId)
        {
            //if (string.IsNullOrWhiteSpace(userId))
            //    throw new ArgumentNullException(_emsg_UserIdIsRequired);

            //string query_string = "select * from Users where USER_ID = @USER_ID";

            //if (_connection.State != ConnectionState.Open)
            //    _connection.Open();

            //if (typeof(TKey).Equals(typeof(Guid)))
            //{
            //    Guid parsegGuid;
            //    if (!Guid.TryParse(userId, out parsegGuid))
            //        throw new ArgumentException(string.Format("'{0}' is not a valid GUID.", new { userId }));

            //    return
            //        Task.Factory.StartNew(
            //            () => { return _connection.Query<User>(query_string, new { userId = parsegGuid }).SingleOrDefault(); });
            //}

            //return
            //    Task.Factory.StartNew(
            //        () => { return _connection.Query<User>(query_string, new { USER_ID = userId }).SingleOrDefault(); });

            return null;
        }

        /// <summary>
        /// Custom implementation for TKey (if TKey is Guid, int etc....
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        //public Task<User> FindByIdAsync(string userId)
        //{
        //    //if (userId == null) throw new ArgumentNullException(_emsg_UserIdIsRequired);

        //    //const string query_string = "select * from Users where USER_ID = @USER_ID";

        //    //if (_connection.State != ConnectionState.Open)
        //    //    _connection.Open();

        //    //if (typeof(TKey).Equals(typeof(String)))
        //    //{
        //    //    return FindByIdAsync(userId.ToString());
        //    //}

        //    //return
        //    //    Task.Factory.StartNew(
        //    //        () => { return _connection.Query<User>(query_string, new { userId }).SingleOrDefault(); });
        //    return null;
        //}

        ///// <summary>
        ///// Finds the by name asynchronous.
        ///// </summary>
        ///// <param name="userName">Name of the user.</param>
        ///// <returns></returns>
        //public Task<User> FindByNameAsync(string userName)
        //{
        //    if (string.IsNullOrWhiteSpace(userName))
        //        throw new ArgumentNullException(_emsg_UserNameIsRequired);

        //    return Task.Factory.StartNew(() =>
        //    {
        //        if (_connection.State != ConnectionState.Open)
        //            _connection.Open();

        //        return null;
        //    });
        //}

        #endregion IUserStore implementation

        #region IUserPasswordStore<User> implementation

       
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            if (user == null)
                throw new ArgumentNullException(_emsg_UserIsRequired);

            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

       
        public Task<string> GetPasswordHashAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(_emsg_UserIsRequired);

            return Task.FromResult(user.PasswordHash);
        }

      
        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        #endregion IUserPasswordStore<AdminUser<TKey>> implementation

        #region IUserSecurityStampStore<User> implementation

     
        public Task SetSecurityStampAsync(User user, string stamp)
        {
            if (user == null)
                throw new ArgumentNullException(_emsg_UserIsRequired);

            user.SecurityStamp = stamp;

            return Task.FromResult(0);
        }

     
        public Task<string> GetSecurityStampAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(_emsg_UserIsRequired);

            return Task.FromResult(user.SecurityStamp);
        }

        #endregion IUserSecurityStampStore<AdminUser<TKey>> implementation

        #region IIdentityRepository

        public async Task<bool> AssignRole(UserRole userRole)
        {
           
            return true;
        }

        public async Task<bool> AssignActivities(UserActivity userActivity)
        {
            return true;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        #endregion IIdentityRepository
    }
}