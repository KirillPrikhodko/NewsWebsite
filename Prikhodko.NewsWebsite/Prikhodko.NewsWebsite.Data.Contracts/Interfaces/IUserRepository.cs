using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByName(string name);
    }
}