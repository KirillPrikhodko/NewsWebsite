using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;

namespace Prikhodko.NewsWebsite.Service.IdentityFramework
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository repository;

        public LoginService(ILoginRepository repository)
        {
            this.repository = repository;
        }
        public async Task<SignInStatus> Login(LoginViewModel model)
        {
            var result = await repository.Login(model);
            return result;
        }

        public async Task Login(ApplicationIdentityUser user, bool isPeristent, bool rememberBrowser)
        {
            await repository.Login(user, isPeristent, rememberBrowser);
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            var result = await repository.ExternalSignInAsync(loginInfo, isPersistent);
            return result;
        }

        public async Task<bool> SendTwoFactorCodeAsync(string selectedProvider)
        {
            var result = await repository.SendTwoFactorCodeAsync(selectedProvider);
            return result;
        }

        public async Task<bool> HasBeenVerifiedAsync()
        {
            return await repository.HasBeenVerifiedAsync();
        }

        public async Task<SignInStatus> TwoFactorSignInAsync(VerifyCodeViewModel model)
        {
            return await repository.TwoFactorSignInAsync(model);
        }

        public async Task<string> GetVerifiedUserIdAsync()
        {
            return await repository.GetVerifiedUserIdAsync();
        }

        public async Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId)
        {
            var result = await repository.GetValidTwoFactorProvidersAsync(userId);
            return result;
        }

        public Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo loginInfo)
        {
            var result = repository.RemoveLoginAsync(userId, loginInfo);
            return result;
        }
    }
}
