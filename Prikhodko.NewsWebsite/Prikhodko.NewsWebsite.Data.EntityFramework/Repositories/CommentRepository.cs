using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPostRepository postRepository;

        public CommentRepository(ApplicationDbContext dbContext, IPostRepository postRepository)
        {
            this.dbContext = dbContext;
            this.postRepository = postRepository;
        }

        public void Add(Comment item)
        {
            if (item == null)
            {
                return;
            }

            item.Post = postRepository.Get(item.PostId);

            dbContext.Comments.Add(item);
        }

        public void Delete(int id)
        {
            if(id <= 0)
            {
                return;
            }

            var toDelete = dbContext.Comments.Find(id);
            if(toDelete != null)
            {
                dbContext.Comments.Remove(toDelete);
            }
        }

        public Comment Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var result = dbContext.Comments.Find(id);
            return result;
        }
    }
}