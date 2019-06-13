using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service.IdentityFramework
{
    public class AccountManageService : IAccountManageService
    {
        private readonly IAccountManageRepository repository;

        public AccountManageService(IAccountManageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo)
        {
            if(string.IsNullOrEmpty(userId) || loginInfo == null)
            {
                return null;
            }
            var result = await repository.AddLoginAsync(userId, loginInfo);
            return result;
        }

        public Task<IdentityResult> AddPasswordAsync(string userId, string password)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            var result = repository.AddPasswordAsync(userId, password);
            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                return null;
            }
            var result = await repository.ChangePasswordAsync(userId, oldPassword, newPassword);
            return result;
        }

        public async Task<ApplicationIdentityUserServiceModel> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            var user = await repository.FindByIdAsync(userId);
            var result = Mapper.Map<ApplicationIdentityUserServiceModel>(user);
            return result;
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            var result = await repository.GetLoginsAsync(userId);
            return result;
        }

        public async Task<string> GetPhoneNumberAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }
            var result = await repository.GetPhoneNumberAsync(userId);
            return result;
        }

        public async Task<bool> GetTwoFactorEnabledAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }
            var result = await repository.GetTwoFactorEnabledAsync(userId);
            return result;
        }

        public async Task SetTwoFactorEnabledAsync(string userId, bool enabled)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }
            await repository.SetTwoFactorEnabledAsync(userId, enabled);
        }
    }
}