using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.EntityFramework.IdentityFramework;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly ApplicationSignInManager signInManager;
        private readonly ApplicationUserManager userManager;

        public LoginRepository(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public async Task<SignInStatus> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            return result;
        }

        public async Task Login(ApplicationUser user, bool isPeristent, bool rememberBrowser)
        {
            await signInManager.SignInAsync(user, isPeristent, rememberBrowser);
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            var result = await signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            return result;
        }

        public async Task<bool> SendTwoFactorCodeAsync(string selectedProvider)
        {
            var result = await signInManager.SendTwoFactorCodeAsync(selectedProvider);
            return result;
        }

        public async Task<bool> HasBeenVerifiedAsync()
        {
            return await signInManager.HasBeenVerifiedAsync();
        }

        public async Task<SignInStatus> TwoFactorSignInAsync(VerifyCodeViewModel model)
        {
            var result = await signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
            return result;
        }

        public async Task<string> GetVerifiedUserIdAsync()
        {
            var userId = await signInManager.GetVerifiedUserIdAsync();
            return userId;
        }

        public async Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId)
        {
            var result = await userManager.GetValidTwoFactorProvidersAsync(userId);
            return result;
        }

        public async Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo loginInfo)
        {
            var result = await userManager.RemoveLoginAsync(userId, loginInfo);
            return result;
        }
    }
}
