using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.IdentityFramework
{
    public class AccountManageRepository : IAccountManageRepository
    {
        private readonly ApplicationUserManager userManager;

        public AccountManageRepository(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
        {
            var result = await userManager.GetLoginsAsync(userId);
            return result;
        }

        public async Task<string> GetPhoneNumberAsync(string userId)
        {
            var result = await userManager.GetPhoneNumberAsync(userId);
            return result;
        }

        public ApplicationIdentityUser FindById(string userId)
        {
            var result = userManager.FindById(userId);
            return result;
        }

        public async Task<bool> GetTwoFactorEnabledAsync(string userId)
        {
            var result = await userManager.GetTwoFactorEnabledAsync(userId);
            return result;
        }

        public async Task SetTwoFactorEnabledAsync(string userId, bool enabled)
        {
            await userManager.SetTwoFactorEnabledAsync(userId, enabled);
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var result = await userManager.ChangePasswordAsync(userId, oldPassword, newPassword);
            return result;
        }

        public async Task<IdentityResult> AddPasswordAsync(string userId, string password)
        {
            var result = await userManager.AddPasswordAsync(userId, password);
            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo)
        {
            var result = await userManager.AddLoginAsync(userId, loginInfo);
            return result;
        }
    }
}