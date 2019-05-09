using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAccountManageService accountManageService;

        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork, IAccountManageService accountManageService)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.accountManageService = accountManageService;
        }
        public void Add(UserServiceModel item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public UserServiceModel FindById(string userId)
        {
            var applicationIdentityUser = accountManageService.FindById(userId);
            UserServiceModel result = null;
            if (applicationIdentityUser != null)
            {
                result = applicationIdentityUser.User;
            }
            return result;
        }

        public UserServiceModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserServiceModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(UserServiceModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}