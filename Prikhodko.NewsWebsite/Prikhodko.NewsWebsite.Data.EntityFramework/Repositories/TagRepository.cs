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

        public Tag Ensure(Tag item)
        {
            var existingTag = dbContext.Tags.FirstOrDefault(x => x.Name == item.Name);
            return existingTag ?? item;
        }

        public IEnumerable<Tag> GetAll()
        {
            var tags = dbContext.Tags.Where(x => x.Posts.Count > 0);
            return tags.ToList();
        }
    }
}