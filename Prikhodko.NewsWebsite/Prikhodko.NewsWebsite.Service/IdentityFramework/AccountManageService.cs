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
            var result = await repository.AddLoginAsync(userId, loginInfo);
            return result;
        }

        public Task<IdentityResult> AddPasswordAsync(string userId, string password)
        {
            var result = repository.AddPasswordAsync(userId, password);
            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var result = await repository.ChangePasswordAsync(userId, oldPassword, newPassword);
            return result;
        }

        public ApplicationIdentityUserServiceModel FindById(string userId)
        {
            var user = repository.FindById(userId);
            var result = Mapper.Map<ApplicationIdentityUser, ApplicationIdentityUserServiceModel>(user);
            return result;
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(string userId)
        {
            var result = await repository.GetLoginsAsync(userId);
            return result;
        }

        public async Task<string> GetPhoneNumberAsync(string userId)
        {
            var result = await repository.GetPhoneNumberAsync(userId);
            return result;
        }

        public async Task<bool> GetTwoFactorEnabledAsync(string userId)
        {
            var result = await repository.GetTwoFactorEnabledAsync(userId);
            return result;
        }

        public async Task SetTwoFactorEnabledAsync(string userId, bool enabled)
        {
            await repository.SetTwoFactorEnabledAsync(userId, enabled);
        }
    }
}