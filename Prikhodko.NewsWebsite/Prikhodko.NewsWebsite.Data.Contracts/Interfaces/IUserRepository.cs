using System;
using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface IUserRepository
    {
        IList<User> GetAll();
        void Delete(string id);
        User FindByName(string name);
        void EditName(string id, string name);
        void EditCountry(string id, string country);
        void EditDateOfBirth(string id, DateTime dateOfBirth);
        void AddRole(string id, string role);
        void RemoveRole(string id, string role);
        IList<string> GetRoles(string id);
    }
}