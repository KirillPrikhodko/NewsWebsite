using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;

namespace Prikhodko.NewsWebsite.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        //TODO: Add transaction mechanism

        private readonly ApplicationDbContext applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void SaveChanges()
        {
            applicationDbContext.SaveChanges();
        }
    }
}
