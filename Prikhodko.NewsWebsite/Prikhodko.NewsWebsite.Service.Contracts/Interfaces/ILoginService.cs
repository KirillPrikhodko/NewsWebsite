﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface ILoginService
    {
        Task<SignInStatus> Login(LoginViewModel model);
        Task Login(ApplicationIdentityUser user, bool isPeristent, bool rememberBrowser);
        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent);

        Task<bool> SendTwoFactorCodeAsync(string selectedProvider);
        Task<bool> HasBeenVerifiedAsync();

        Task<SignInStatus> TwoFactorSignInAsync(VerifyCodeViewModel model);
        Task<string> GetVerifiedUserIdAsync();
        Task<IList<string>> GetValidTwoFactorProvidersAsync(string userId);
        Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo loginInfo);
    }
}
