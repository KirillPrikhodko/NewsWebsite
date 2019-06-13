using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface ITagRepository : IEnsurable<Tag>
    {
        IEnumerable<Tag> GetAll();
    }
}