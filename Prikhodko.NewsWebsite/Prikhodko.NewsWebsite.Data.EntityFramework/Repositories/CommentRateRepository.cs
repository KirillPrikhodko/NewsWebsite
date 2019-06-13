using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class CommentRateRepository : ICommentRateRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICommentRepository commentRepository;

        public CommentRateRepository(ApplicationDbContext dbContext, ICommentRepository commentRepository)
        {
            this.dbContext = dbContext;
            this.commentRepository = commentRepository;
        }
        public void Add(CommentRate item)
        {
            if(item == null)
            {
                return;
            }

            item.Comment = commentRepository.Get(item.CommentId);
            dbContext.CommentRates.Add(item);
        }
    }
}