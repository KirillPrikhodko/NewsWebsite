using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Data.EntityFramework.IdentityFramework;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ApplicationUserManager userManager;

        public RegisterRepository(ApplicationUserManager userManager, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return null;
            }
            return await userManager.GenerateEmailConfirmationTokenAsync(Id);
        }

        public async Task SendEmailAsync(string userId, string subject, string body)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
            {
                return;
            }
            await userManager.SendEmailAsync(userId, subject, body);
        }

        public async Task<IdentityResult> Register(RegisterViewModel model, ApplicationIdentityUser user)
        {
            if (model == null || user == null)
            {
                return null;
            }
            user.User = new User() { Id = user.Id, DateOfBirth = null };
            user.IsEnabled = true;
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, "Reader");
                if (user.UserName.ToLower() == "admin")
                {
                    userManager.AddToRoles(user.Id, "Writer", "Admin");
                }
            }
            return result;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                return null;
            }
            var result = await userManager.ConfirmEmailAsync(userId, code);
            return result;
        }
    }
}
