using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface IRegisterService
    {
        Task<IdentityResult> Register(RegisterViewModel model, ApplicationUser user);
    }
}
