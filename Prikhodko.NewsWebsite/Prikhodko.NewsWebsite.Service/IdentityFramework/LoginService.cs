using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;
using LoginViewModel = Prikhodko.NewsWebsite.CommonModels.LoginViewModel;

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
            if(model == null)
            {
                return SignInStatus.Failure;
            }
            var result = await repository.Login(model);
            return result;
        }

        public async Task Login(ApplicationIdentityUserServiceModel userViewModel, bool isPeristent, bool rememberBrowser)
        {
            if (userViewModel == null)
            {
                return;
            }
            var user = Mapper.Map<ApplicationIdentityUser>(userViewModel);
            await repository.Login(user, isPeristent, rememberBrowser);
        }

        public async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            if (loginInfo == null)
            {
                return SignInStatus.Failure;
            }
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
            if(model == null)
            {
                return SignInStatus.Failure;
            }
            return await repository.TwoFactorSignInAsync(model);
        }

        public async Task<string> GetVerifiedUserIdAsync()
        {
            return await repository.GetVerifiedUserIdAsync();
        }

        public async Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId)
        {
            if(string.IsNullOrEmpty(userId))
            {
                return null;
            }
            var result = await repository.GetValidTwoFactorProvidersAsync(userId);
            return result;
        }

        public Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo loginInfo)
        {
            if(string.IsNullOrEmpty(userId) || loginInfo == null)
            {
                return null;
            }
            var result = repository.RemoveLoginAsync(userId, loginInfo);
            return result;
        }
    }
}
