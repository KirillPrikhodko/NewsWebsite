using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class CommentRateRepository : IRepository<CommentRate>
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IRepository<Comment> commentRepository;

        public CommentRateRepository(ApplicationDbContext dbContext, IRepository<Comment> commentRepository)
        {
            this.dbContext = dbContext;
            this.commentRepository = commentRepository;
        }
        public void Add(CommentRate item)
        {
            item.Comment = commentRepository.Get(item.Comment.Id);
            dbContext.CommentRates.Add(item);
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public CommentRate Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CommentRate> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(CommentRate item)
        {
            throw new System.NotImplementedException();
        }
    }
}