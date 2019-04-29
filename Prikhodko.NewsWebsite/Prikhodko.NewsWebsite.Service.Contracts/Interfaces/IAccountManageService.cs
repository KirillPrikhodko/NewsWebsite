using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface IAccountManageService
    {
        Task<string> GetPhoneNumberAsync(string userId);
        Task<IList<UserLoginInfo>> GetLoginsAsync(string userId);
        Task<bool> GetTwoFactorEnabledAsync(string userId);
        ApplicationUser FindById(string userId);
        Task SetTwoFactorEnabledAsync(string userId, bool enabled);
        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<IdentityResult> AddPasswordAsync(string userId, string password);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo);
    }
}