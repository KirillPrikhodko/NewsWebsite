﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            if (string.IsNullOrEmpty(id) || country == null) //unlike Id, country can be empty
            {
                return;
            }

            repository.EditCountry(id, country);
            unitOfWork.SaveChanges();
        }

        public void EditDateOfBirth(string id, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(id) || dateOfBirth < DateTime.Today.AddYears(-150))
            {
                return;
            }

            repository.EditDateOfBirth(id, dateOfBirth);
            unitOfWork.SaveChanges();
        }

        public void EditName(string id, string name)
        {
            if (string.IsNullOrEmpty(id) || name == null) //unlike Id, name can be empty
            {
                return;
            }

            repository.EditName(id, name);
            unitOfWork.SaveChanges();
        }

        public async Task<UserServiceModel> FindByIdAsync(string userId)
        {
            var applicationIdentityUser = await accountManageService.FindByIdAsync(userId);
            UserServiceModel result = null;
            if (applicationIdentityUser != null)
            {
                result = applicationIdentityUser.User;
                result.ApplicationIdentityUser.Roles = repository.GetRoles(result.Id);
            }
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