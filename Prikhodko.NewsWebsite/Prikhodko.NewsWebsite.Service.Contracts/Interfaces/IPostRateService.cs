using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface IPostRateService
    {
        void Add(PostRateServiceModel item);
    }
}