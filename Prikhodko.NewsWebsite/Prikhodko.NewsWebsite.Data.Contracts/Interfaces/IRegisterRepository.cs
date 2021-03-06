﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface IRegisterRepository
    {
        Task<IdentityResult> Register(RegisterViewModel model, ApplicationIdentityUser user);
        Task<string> GenerateEmailConfirmationTokenAsync(string Id);
        Task SendEmailAsync(string userId, string subject, string body);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string code);
    }
}
