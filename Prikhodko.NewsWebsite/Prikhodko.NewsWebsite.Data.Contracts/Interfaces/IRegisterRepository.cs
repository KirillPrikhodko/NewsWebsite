using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface IRegisterRepository
    {
        Task<IdentityResult> Register(RegisterViewModel model, ApplicationIdentityUser user);
    }
}
