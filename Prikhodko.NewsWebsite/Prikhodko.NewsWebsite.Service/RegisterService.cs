using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;

namespace Prikhodko.NewsWebsite.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository repository;

        public RegisterService(IRegisterRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IdentityResult> Register(RegisterViewModel model, ApplicationUser user)
        {
            var result = await repository.Register(model, user);
            return result;
        }
    }
}
