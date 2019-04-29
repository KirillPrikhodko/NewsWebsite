using System;
using System.Collections.Generic;
using Prikhodko.NewsWebsite.CommonModels;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
//using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;

namespace Prikhodko.NewsWebsite.Service.IdentityFramework
{
    public class ApplicationUserService : IService<ApplicationUser>
    {
        private readonly IRepository<ApplicationUser> repository;
        private readonly IUnitOfWork unitOfWork;

        public ApplicationUserService(IRepository<ApplicationUser> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public void Add(ApplicationUser item)
        {
            repository.Add(item);
            unitOfWork.SaveChanges();
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
