using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPostRepository postRepository;
        private readonly IUserRepository userRepository;

        public CommentRepository(ApplicationDbContext dbContext, IPostRepository postRepository, IUserRepository userRepository)
        {
            this.dbContext = dbContext;
            this.postRepository = postRepository;
            this.userRepository = userRepository;
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
            var toDelete = dbContext.Comments.Find(id);
            if(toDelete != null)
            {
                dbContext.Comments.Remove(toDelete);
            }
        }

        public Comment Get(int id)
        {
            var result = dbContext.Comments.Find(id);
            return result;
        }

        public IEnumerable<Comment> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Comment item)
        {
            throw new System.NotImplementedException();
        }
    }
}