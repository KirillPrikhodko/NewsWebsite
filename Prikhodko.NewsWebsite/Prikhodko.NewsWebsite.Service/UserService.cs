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
        public void Add(UserViewModel item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public UserViewModel FindById(string userId)
        {
            var applicationIdentityUser = accountManageService.FindById(userId);
            UserViewModel result = null;
            if (applicationIdentityUser != null)
            {
                result = applicationIdentityUser.User;
            }
            return result;
        }

        public UserViewModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(UserViewModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}