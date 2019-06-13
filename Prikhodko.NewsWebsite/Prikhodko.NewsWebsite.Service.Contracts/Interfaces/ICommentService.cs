using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service.Contracts.Interfaces
{
    public interface ICommentService
    {
        void Add(CommentServiceModel item);
        CommentServiceModel Get(int id);
        void Delete(int id);
    }
}