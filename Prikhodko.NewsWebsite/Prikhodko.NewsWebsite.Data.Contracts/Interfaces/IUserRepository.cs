using System;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByName(string name);
        void EditName(string id, string name);
        void EditCountry(string id, string country);
        void EditDateOfBirth(string id, DateTime dateOfBirth);
    }
}