using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface IUserService : IService<UserServiceModel>
    {
        UserServiceModel FindById(string userId);
        UserServiceModel FindByName(string name);
    }
}