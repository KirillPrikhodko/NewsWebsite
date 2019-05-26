using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using System.Data.Entity;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRepository<User> userRepository;
        private readonly ITagRepository tagRepository;
        private readonly ICategoryRepository categoryRepository;

        public PostRepository(ApplicationDbContext dbContext, IRepository<User> userRepository, ITagRepository tagRepository, ICategoryRepository categoryRepository)
        {
            this.dbContext = dbContext;
            this.userRepository = userRepository;
            this.tagRepository = tagRepository;
            this.categoryRepository = categoryRepository;
        }
        public void Add(Post item)
        {
            if (item == null || item.Id < 0)
            {
                return;
            }

            item.Author = dbContext.Users.Find(item.AuthorId).User;
            item.AvgRate = GetAvgPostRate(item);
            for(int i = 0; i < item.Tags.Count(); i++)
            {
                item.Tags[i] = tagRepository.Ensure(item.Tags[i]);
            }
            item.Category = categoryRepository.Ensure(item.Category);
            dbContext.Posts.Add(item);
        }

        public double? GetAvgPostRate(Post post)
        {
            double? result = 0;
            var quantity = post.Rates.Count;
            for (int i = 0; i < quantity; i++)
            {
                result += post.Rates[i].Value;
            }

            return quantity == 0 ? null : result / quantity;
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            var postToDelete = dbContext.Posts.Find(id);
            if (postToDelete != null)
            {
                dbContext.Posts.Remove(postToDelete);
            }
        }

        public Post Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var result = dbContext.Posts.Find(id);
            return result;
        }

        public IEnumerable<Post> GetAll()
        {
            var result = dbContext.Posts.ToList();
            return result;
        }

        public IEnumerable<Post> GetBest(double minimumRate, int amount)
        {
            if (minimumRate < 0 || double.IsInfinity(minimumRate) || double.IsNaN(minimumRate) || amount <= 0)
            {
                return null;
            }
            var result = dbContext.Posts.Where(x => x.AvgRate != null && x.AvgRate >= minimumRate).Take(amount).OrderByDescending(x => x.AvgRate).ToList();
            return result;
        }

        public IEnumerable<Post> GetByTag(Tag tag)
        {
            if (tag == null)
            {
                return null;
            }
            var result = dbContext.Posts.Where(x => x.Tags.Contains(tag)).ToList();
            return result;
        }

        public IEnumerable<Post> GetFresh(int amount)
        {
            var expirationDate = DateTime.Now.AddDays(-1);
            var result = dbContext.Posts.Where(x => x.Created >= expirationDate).Take(amount).OrderBy(x => x.Created).ToList();
            return result;
        }

        public void Update(Post item)
        {
            if (item == null)
            {
                return;
            }
            var postToUpdate = dbContext.Posts.Find(item.Id);
            if (postToUpdate != null)
            {
                item.Update(postToUpdate);
            }
        }
    }
}