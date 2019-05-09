using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class RegisterService : IRegisterService
    {
        private readonly IRegisterRepository repository;

        public RegisterService(IRegisterRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IdentityResult> Register(RegisterViewModel model, ApplicationIdentityUserServiceModel userViewModel)
        {
            var user = Mapper.Map<ApplicationIdentityUser>(userViewModel);
            var result = await repository.Register(model, user);
            userViewModel.Id = user.User.Id;
            return result;
        }
    }
}
