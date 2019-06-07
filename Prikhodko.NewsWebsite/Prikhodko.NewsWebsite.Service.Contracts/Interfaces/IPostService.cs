using System.Collections.Generic;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface IPostService : IService<PostServiceModel>
    {
        IEnumerable<PostServiceModel> GetByTag(TagServiceModel tagModel);
        IEnumerable<PostServiceModel> GetFresh();
        IEnumerable<PostServiceModel> GetBest(double minimumRate);
    }
}