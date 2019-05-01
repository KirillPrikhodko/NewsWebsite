using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class UserService : IService<UserViewModel>
    {
        private readonly IRepository<User> repository;

        public UserService(IRepository<User> repository)
        {
            this.repository = repository;
        }
        public void Add(UserViewModel item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
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