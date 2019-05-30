using System;
using System.Collections.Generic;
using System.Linq;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(User item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public User FindByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            var result = dbContext.AppUsers.Where(x => x.ApplicationIdentityUser.UserName == name).FirstOrDefault();
            return result;
        }

        public User Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(User item)
        {
            throw new System.NotImplementedException();
        }

        public void EditName(string id, string name)
        {
            var user = dbContext.AppUsers.Find(id);
            if (user == null || string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("no user with requested id");
            }

            user.Name = name;
        }

        public void EditCountry(string id, string country)
        {
            var user = dbContext.AppUsers.Find(id);
            if (user == null || string.IsNullOrEmpty(country))
            {
                throw new ArgumentException("no user with requested id");
            }

            user.Country = country;
        }

        public void EditDateOfBirth(string id, DateTime dateOfBirth)
        {
            var user = dbContext.AppUsers.Find(id);
            if (user == null)
            {
                throw new ArgumentException("no user with requested id");
            }

            user.DateOfBirth = dateOfBirth;
        }
    }
}