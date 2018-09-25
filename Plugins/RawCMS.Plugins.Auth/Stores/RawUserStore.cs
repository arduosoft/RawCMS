using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Library.Core;
using RawCMS.Library.Service;

namespace RawCMS.Plugins.Auth.Stores
{
    public class RawUserStore : IUserStore<IdentityUser>, 
        IRequireApp, 
        IRequireCrudService, 
        IUserPasswordStore<IdentityUser>, 
        IPasswordValidator<IdentityUser>,
        IPasswordHasher<IdentityUser>
    {
        public Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
          
        }

        public Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return FindByNameAsync(userId, cancellationToken);
        }

        public async Task<IdentityUser>  FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
           
                return new IdentityUser()
                {
                    UserName = normalizedUserName,
                    PasswordHash = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes("XYZ")),
                    NormalizedUserName=normalizedUserName
                    

                };
            
        }

        public Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return user.UserName;
        }

        public async Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return user.UserName;
        }

        private AppEngine appEngine;
        public void SetAppEngine(AppEngine manager)
        {
            this.appEngine = manager;

        }

        public Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected CRUDService service;
        public void SetCRUDService(CRUDService service)
        {
            this.service = service;
        }

        public async Task SetPasswordHashAsync(IdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
        }

        public async Task<string> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return user.PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return true;
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user, string password)
        {

            return IdentityResult.Success;
            
        }

        public string HashPassword(IdentityUser user, string password)
        {
            throw new NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(IdentityUser user, string hashedPassword, string providedPassword)
        {
            return PasswordVerificationResult.Success;
        }
    }
}
