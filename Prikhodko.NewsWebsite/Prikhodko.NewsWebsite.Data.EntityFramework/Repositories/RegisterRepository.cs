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
        public async Task<IdentityResult> Register(RegisterViewModel model, ApplicationIdentityUser user)
        {
            user.User = new User() { Id = user.Id };
            var result = await userManager.CreateAsync(user, model.Password);
            return result;
        }
    }
}
