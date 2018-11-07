using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Library.DataModel;
using RawCMS.Library.Service;
using RawCMS.Plugins.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RawCMS.Plugins.Core.Stores
{
    public class RawClaimsFactory : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>
    {
        public async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            JObject userObj = JObject.FromObject(user);
            foreach (JProperty key in userObj.Properties())
            {
                if (key.HasValues && !key.Name.Contains("Password"))
                {
                    //TODO: manage metadata
                    claims.Add(new Claim(key.Name, key.Value.ToString()));
                }
            }
            return claims;
        }

        public RawClaimsFactory(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(IdentityUser user)
        {
            ClaimsPrincipal principal = await base.CreateAsync(user);
            //principal.Identity.(await GetClaimsAsync(user));
            return principal;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            ClaimsIdentity principal = await base.GenerateClaimsAsync(user);
            principal.AddClaims(await GetClaimsAsync(user));
            return principal;
        }
    }

    public class RawUserStore : IUserStore<IdentityUser>,
        IRequireApp,
        IRequireCrudService,
        IUserPasswordStore<IdentityUser>,
        IPasswordValidator<IdentityUser>,
        IUserClaimStore<IdentityUser>,
        IPasswordHasher<IdentityUser>,
        IProfileService,
        IRequireLog
    {
        private ILogger logger;
        private CRUDService service;
        private const string collection = "_users";

        private AppEngine appEngine;

        public void SetAppEngine(AppEngine manager)
        {
            appEngine = manager;
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
            service.Insert(collection, JObject.FromObject(user));
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            service.Delete(collection, user.Id);
            return IdentityResult.Success;
        }

        public void Dispose()
        {
        }

        public async Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            JObject result = service.Get(collection, userId);
            return result.ToObject<IdentityUser>();
        }

        public async Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            //return new IdentityUser()
            //{
            //    UserName = normalizedUserName,
            //    PasswordHash = Convert.ToBase64String(System.Text.UTF8Encoding.UTF8.GetBytes("XYZ")),
            //    NormalizedUserName=normalizedUserName,
            //    Email="test@test.it",
            //    NormalizedEmail="test@test.it"
            //};

            DataQuery query = new DataQuery()
            {
                RawQuery = JsonConvert.SerializeObject(new { NormalizedUserName = normalizedUserName })
            };

            ItemList result = service.Query(collection, query);
            if (result.TotalCount == 0)
            {
                return null;
            }

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

        public async Task InitData()
        {
            IdentityUser user = await FindByNameAsync("BOB", CancellationToken.None);
            if (user == null)
            {
                IdentityUser userToAdd = new IdentityUser()
                {
                    UserName = "bob",
                    NormalizedUserName = "BOB",
                    Email = "test@test.it",
                    NormalizedEmail = "test@test.it",
                    PasswordHash = ComputePasswordHash("XYZ")
                };

                await CreateAsync(userToAdd, CancellationToken.None);
            }
        }

        public async Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
        }

        public async Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            service.Update(collection, JObject.FromObject(user), true);
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
            foreach (IPasswordValidator<IdentityUser> val in manager.PasswordValidators)
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
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };
            JObject userObj = JObject.FromObject(user);
            foreach (JProperty key in userObj.Properties())
            {
                if (key.HasValues && !key.Name.Contains("Password"))//TODO: implement blacklists
                {
                    //TODO: manage metadata
                    claims.Add(new Claim(key.Name, key.Value.ToString()));
                }
            }
            return claims;
        }

        public async Task AddClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (Claim claim in claims)
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

        public async Task<ClaimsPrincipal> CreateAsync(IdentityUser user)
        {
            ClaimsIdentity id = new ClaimsIdentity(user.UserName, ClaimTypes.NameIdentifier, ClaimTypes.Role);
            id.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserName));

            if (user.Roles != null)
            {
                id.AddClaim(new Claim(ClaimTypes.Role, string.Join(",", user.Roles)));
            }
            id.AddClaims(await GetClaimsAsync(user, CancellationToken.None));

            ClaimsPrincipal userprincipal = new ClaimsPrincipal();
            userprincipal.AddIdentity(id);
            return userprincipal;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            Claim userid = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");
            if (userid != null)
            {
                IdentityUser user = await FindByIdAsync(userid.Value, CancellationToken.None);
                IList<Claim> tokens = await GetClaimsAsync(user, CancellationToken.None);

                context.IssuedClaims.AddRange(tokens);
            }
            //context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
        }
    }
}