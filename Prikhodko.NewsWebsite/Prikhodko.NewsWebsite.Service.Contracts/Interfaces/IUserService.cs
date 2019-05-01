using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface IUserService : IService<UserViewModel>
    {
        UserViewModel FindById(string userId);
    }
}