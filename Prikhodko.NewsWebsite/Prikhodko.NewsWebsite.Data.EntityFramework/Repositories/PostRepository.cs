using System.Collections.Generic;
using System.Linq;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private readonly ApplicationDbContext applicationDbContext;

        public PostRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
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
            var postToDelete = applicationDbContext.Posts.Find(id);
            if (postToDelete != null)
            {
                applicationDbContext.Posts.Remove(postToDelete);
            }
        }

        public Post Get(int id)
        {
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
            var postToUpdate = applicationDbContext.Posts.Find(item.Id);
            item.Update(ref item);
        }
    }
}