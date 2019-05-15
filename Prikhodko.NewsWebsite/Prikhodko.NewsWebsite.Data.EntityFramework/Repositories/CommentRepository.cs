using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<User> userRepository;

        public CommentRepository(ApplicationDbContext dbContext, IRepository<Post> postRepository, IRepository<User> userRepository)
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
            throw new System.NotImplementedException();
        }

        public Comment Get(int id)
        {
            throw new System.NotImplementedException();
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