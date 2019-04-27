using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
//using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class ApplicationUserRepository : IRepository<ApplicationUser>
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ApplicationUserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public void Add(ApplicationUser item)
        {

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser item)
        {
            throw new NotImplementedException();
        }
    }
}
