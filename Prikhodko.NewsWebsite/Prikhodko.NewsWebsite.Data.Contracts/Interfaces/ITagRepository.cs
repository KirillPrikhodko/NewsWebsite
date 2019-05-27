using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface ITagRepository : IRepository<Tag>, IEnsurable<Tag>
    {
        IEnumerable<Tag> GetAmount(int amount);
    }
}