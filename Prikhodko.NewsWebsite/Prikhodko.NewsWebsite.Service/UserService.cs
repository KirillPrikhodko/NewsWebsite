using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAccountManageService accountManageService;

        public UserService(IUserRepository repository, IUnitOfWork unitOfWork, IAccountManageService accountManageService)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.accountManageService = accountManageService;
        }

        public void AddRole(string id, string role)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
            {
                return;
            }
            repository.AddRole(id, role);
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("invalid id");
            }
            repository.Delete(id);
            unitOfWork.SaveChanges();
        }

        public void EditCountry(string id, string country)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(country))
            {
                throw new ArgumentException("invalid id or country");
            }

            repository.EditCountry(id, country);
            unitOfWork.SaveChanges();
        }

        public void EditDateOfBirth(string id, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(id) || dateOfBirth < DateTime.Today.AddYears(-150))
            {
                throw new ArgumentException("invalid id or date");
            }

            repository.EditDateOfBirth(id, dateOfBirth);
            unitOfWork.SaveChanges();
        }

        public void EditName(string id, string name)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("invalid id or name");
            }

            repository.EditName(id, name);
            unitOfWork.SaveChanges();
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

        public UserServiceModel FindByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var user = repository.FindByName(name);
            var result = /*user == null ? null : */Mapper.Map<UserServiceModel>(repository.FindByName(name));
            return result;
        }

        public IList<UserServiceModel> GetAll()
        {
            var users = repository.GetAll();
            var result = users.Select(x => Mapper.Map<UserServiceModel>(x)).Where(x => x.ApplicationIdentityUser != null).ToList();
            foreach (var user in result)
            {
                user.ApplicationIdentityUser.Roles = repository.GetRoles(user.Id);
            }
            return result;
        }

        public void RemoveRole(string id, string role)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
            {
                return;
            }
            repository.RemoveRole(id, role);
        }
    }
}