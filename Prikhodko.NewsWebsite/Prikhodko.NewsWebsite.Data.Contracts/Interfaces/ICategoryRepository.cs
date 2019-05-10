using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>, IEnsurable<Category>
    {
        
    }
}