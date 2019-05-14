using System;
using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class PostRateRepository : IRepository<PostRate>
    {
        private readonly ApplicationDbContext dbContext;

        public PostRateRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(PostRate item)
        {
            if (item != null && item.Value > 0)
            {
                item.Author = dbContext.Users.Find(item.Author.Id).User;
                dbContext.PostRates.Add(item);
            }
            else
            {
                throw new IndexOutOfRangeException("someone tried to rate a post 0 or less");
            }
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public PostRate Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PostRate> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(PostRate item)
        {
            throw new System.NotImplementedException();
        }
    }
}