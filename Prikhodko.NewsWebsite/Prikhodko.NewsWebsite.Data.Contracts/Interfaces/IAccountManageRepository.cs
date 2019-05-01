using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface IAccountManageRepository
    {
        Task<string> GetPhoneNumberAsync(string userId);
        Task<IList<UserLoginInfo>> GetLoginsAsync(string userId);
        ApplicationIdentityUser FindById(string userId);
        Task<bool> GetTwoFactorEnabledAsync(string userId);
        Task SetTwoFactorEnabledAsync(string userId, bool enabled);
        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<IdentityResult> AddPasswordAsync(string userId, string password);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo);
    }
}