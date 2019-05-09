using System.Collections.Generic;
using System.Linq;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class TagRepository : IRepository<Tag>
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

        public Tag Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Tag> GetAll()
        {
            var tags = dbContext.Tags.ToList();
            return tags;
        }

        public void Update(Tag item)
        {
            throw new System.NotImplementedException();
        }
    }
}