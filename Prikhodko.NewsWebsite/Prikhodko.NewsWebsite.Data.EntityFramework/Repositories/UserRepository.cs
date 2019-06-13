using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Data.EntityFramework.IdentityFramework;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ApplicationUserManager userManager;

        public UserRepository(ApplicationDbContext dbContext, ApplicationUserManager userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            var toRemove = dbContext.AppUsers.Find(id);
            if (toRemove != null)
            {
                toRemove.ApplicationIdentityUser.IsEnabled = false;                
            }
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

        public IList<User> GetAll()
        {
            var users = dbContext.AppUsers.Where(x => x.ApplicationIdentityUser != null);
            return users.ToList();
        }

        public void EditName(string id, string name)
        {
            if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                return;
            }

            var user = dbContext.AppUsers.Find(id);
            if (user == null)
            {
                return;
            }

            user.Name = name;
        }

        public void EditCountry(string id, string country)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(country))
            {
                return;
            }

            var user = dbContext.AppUsers.Find(id);
            if (user == null)
            {
                return;
            }

            user.Country = country;
        }

        public void EditDateOfBirth(string id, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(id))
            {
                return;
            }

            var user = dbContext.AppUsers.Find(id);
            if (user == null)
            {
                return;
            }

            user.DateOfBirth = dateOfBirth;
        }

        public void AddRole(string id, string role)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
            {
                return;
            }
            userManager.AddToRole(id, role);
        }

        public void RemoveRole(string id, string role)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
            {
                return;
            }

            if (dbContext.Users.Find(id).UserName == "admin")
            {
                return; //no one is allowed to remove permissions from admin, even admin
            }
            userManager.RemoveFromRole(id, role);
        }

        public IList<string> GetRoles(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return null;
            }
            return userManager.GetRoles(id);
        }
    }
}