using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.EntityFramework.IdentityFramework;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly ApplicationUserManager userManager;

        public RegisterRepository(ApplicationUserManager userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IdentityResult> Register(RegisterViewModel model, ApplicationIdentityUser user)
        {
            var result = await userManager.CreateAsync(user, model.Password);
            return result;
        }
}
}
