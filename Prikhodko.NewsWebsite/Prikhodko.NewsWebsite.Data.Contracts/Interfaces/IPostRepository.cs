using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetByTag(Tag tag);
        IEnumerable<Post> GetFresh();
        IEnumerable<Post> GetBest(double minimumRate);

        double? GetAvgPostRate(Post item);
    }
}