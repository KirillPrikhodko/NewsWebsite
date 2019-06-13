using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface ICommentRepository
    {
        void Add(Comment item);
        void Delete(int id);
        Comment Get(int id);
    }
}