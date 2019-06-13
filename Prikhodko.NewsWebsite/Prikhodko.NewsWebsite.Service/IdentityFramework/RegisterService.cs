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

        public Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
            {
                return null;
            }

            return repository.ConfirmEmailAsync(userId, code);
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return null;
            }

            return repository.GenerateEmailConfirmationTokenAsync(Id);
        }

        public async Task<IdentityResult> Register(RegisterViewModel model, ApplicationIdentityUserServiceModel userViewModel)
        {
            if(model == null || userViewModel == null)
            {
                return null;
            }
            var user = Mapper.Map<ApplicationIdentityUser>(userViewModel);
            var result = await repository.Register(model, user);
            userViewModel.Id = user.User.Id;
            return result;
        }

        public async Task SendEmailAsync(string userId, string subject, string body)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
            {
                return;
            }

            await repository.SendEmailAsync(userId, subject, body);
        }
    }
}
