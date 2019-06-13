using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.Contracts.Interfaces
{
    public interface ICommentRateRepository
    {
        void Add(CommentRate item);
    }
}