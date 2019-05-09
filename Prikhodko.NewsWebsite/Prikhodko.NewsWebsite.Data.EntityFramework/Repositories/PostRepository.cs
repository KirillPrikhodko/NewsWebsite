using System.Collections.Generic;
using System.Linq;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IRepository<User> userRepository;

        public PostRepository(ApplicationDbContext applicationDbContext, IRepository<User> userRepository)
        {
            this.applicationDbContext = applicationDbContext;
            this.userRepository = userRepository;
        }
        public void Add(Post item)
        {
            if (item == null || item.Id < 0)
            {
                return;
            }
            applicationDbContext.Posts.Add(item);
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            var postToDelete = applicationDbContext.Posts.Find(id);
            if (postToDelete != null)
            {
                applicationDbContext.Posts.Remove(postToDelete);
            }
        }

        public Post Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var result = applicationDbContext.Posts.Find(id);
            return result;
        }

        public IEnumerable<Post> GetAll()
        {
            var result = applicationDbContext.Posts.ToList();
            return result;
        }

        public void Update(Post item)
        {
            if (item == null)
            {
                return;
            }
            var postToUpdate = applicationDbContext.Posts.Find(item.Id);
            if (postToUpdate != null)
            {
                item.Update(postToUpdate);
            }
        }
    }
}