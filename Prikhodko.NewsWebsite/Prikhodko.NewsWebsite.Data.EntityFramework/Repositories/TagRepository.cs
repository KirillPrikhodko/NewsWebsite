using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TagRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Tag item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public Tag Ensure(Tag item)
        {
            var existingTag = dbContext.Tags.FirstOrDefault(x => x.Name == item.Name);
            return existingTag ?? item;
        }

        public Tag Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Tag> GetAll()
        {
            var tags = dbContext.Tags.ToList();
            return tags;
        }

        public IEnumerable<Tag> GetAmount(int amount)
        {
            if (amount <= 0)
            {
                return null;
            }
            var result = dbContext.Tags.OrderBy(x => x.Posts.Count).Take(amount).ToList();
            return result;
        }

        public void Update(Tag item)
        {
            throw new System.NotImplementedException();
        }
    }
}