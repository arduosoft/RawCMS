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
using System.Security.Claims;
using RawCMS.Plugins.Core.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using RawCMS.Library.DataModel;
using Newtonsoft.Json;

namespace RawCMS.Plugins.Core.Stores
{
    public class RawUserStore : IUserStore<IdentityUser>, 
        IRequireApp, 
        IRequireCrudService, 
        IUserPasswordStore<IdentityUser>, 
        IPasswordValidator<IdentityUser>,
        IUserClaimStore<IdentityUser>,  
        IPasswordHasher<IdentityUser>,
        IRequireLog
    {
        ILogger logger;
        CRUDService service;
        const string collection = "_users";

        private AppEngine appEngine;
        public void SetAppEngine(AppEngine manager)
        {
            this.appEngine = manager;

        }

        public void SetCRUDService(CRUDService service)
        {
            this.service = service;

        }

        public void SetLogger(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = user.UserName.ToUpper();
            this.service.Insert(collection, JObject.FromObject(user));
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            this.service.Delete(collection, user.Id);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
          
        }

        public async Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var result= this.service.Get(collection, userId);
            return result.ToObject<IdentityUser>();
        }

        public async Task<IdentityUser>  FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {

            //return new IdentityUser()
            //{
            //    UserName = normalizedUserName,
            //    PasswordHash = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes("XYZ")),
            //    NormalizedUserName=normalizedUserName,
            //    Email="test@test.it",
            //    NormalizedEmail="test@test.it"
            //};

           

            var query = new DataQuery()
            {
                RawQuery = JsonConvert.SerializeObject(new { NormalizedUserName = normalizedUserName })
            };

            var result = this.service.Query(collection, query);
            if (result.TotalCount == 0) return null;
            return result.Items.First.ToObject<IdentityUser>();

        }

        public async Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return user.NormalizedUserName;
        }

        public async Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return user.Id;
        }

        public async Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return user.UserName;
        }

      

        public async Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
        }

        public  async Task InitData()
        {
            var user = await FindByNameAsync("BOB", CancellationToken.None);
            if (user == null)
            {
                var userToAdd =new IdentityUser()
                {
                    UserName = "bob",                 
                    NormalizedUserName = "BOB",
                    Email = "test@test.it",
                    NormalizedEmail = "test@test.it",
                    PasswordHash= ComputePasswordHash("XYZ")

                };
                
                await this.CreateAsync(userToAdd, CancellationToken.None);
            }
        }

        public async Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
        }

        public async Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            this.service.Update(collection, JObject.FromObject(user),true);
            return IdentityResult.Success;
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

            
            foreach (var val in manager.PasswordValidators)
            {
                if (await val.ValidateAsync(manager, user, password) != IdentityResult.Success)
                {
                    return IdentityResult.Failed(new IdentityError[] {
                        new IdentityError()
                        {
                            Code="VALIDATION FAILED",
                            Description=val.ToString()
                        }
                    });
                }
            }
            return IdentityResult.Success;
            
        }

        public string HashPassword(IdentityUser user, string password)
        {
            return ComputePasswordHash(password);
        }

        private string ComputePasswordHash(string password)
        {
            return Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes(password));
        }

        public PasswordVerificationResult VerifyHashedPassword(IdentityUser user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == HashPassword(user, providedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }

        public async Task<IList<Claim>> GetClaimsAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            JObject userObj = JObject.FromObject(user);
            foreach (var key in userObj.Properties())
            {
                if (key.HasValues)
                {
                    //TODO: manage metadata
                    claims.Add(new Claim(key.Name, key.Value.ToString()));
                }
            }
            return claims;
        }

        public async Task AddClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {
                //TODO:
            }
        }

        public async Task ReplaceClaimAsync(IdentityUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            //TODO:
        }

        public async Task RemoveClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            //TODO:
        }

        public async Task<IList<IdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            //TODO:
            return null;
        }
    }
}
