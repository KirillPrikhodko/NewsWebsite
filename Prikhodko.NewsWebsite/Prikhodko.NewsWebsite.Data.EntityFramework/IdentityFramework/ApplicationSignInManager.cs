using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.IdentityFramework
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationIdentityUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationIdentityUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public override Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe, bool shouldLockout)
        {
            var user = UserManager.FindByNameAsync(userName).Result;

            if (user != null)
            {
                if ((user.IsEnabled.HasValue && !user.IsEnabled.Value) || !user.IsEnabled.HasValue)
                {
                    return Task.FromResult<SignInStatus>(SignInStatus.LockedOut);
                }
            }

            return base.PasswordSignInAsync(userName, password, rememberMe, shouldLockout);
        }
    }
}
