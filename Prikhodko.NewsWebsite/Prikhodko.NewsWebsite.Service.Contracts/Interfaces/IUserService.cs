using System;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface IUserService : IService<UserServiceModel>
    {
        UserServiceModel FindById(string userId);
        UserServiceModel FindByName(string name);

        void EditName(string id, string name);
        void EditCountry(string id, string country);
        void EditDateOfBirth(string id, DateTime dateOfBirth);
        void AddRole(string id, string role);
        void RemoveRole(string id, string role);
    }
}