using System.Collections.Generic;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class CommentRateService : ICommentRateService
    {
        private readonly ICommentService commentService;
        private readonly ICommentRateRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public CommentRateService(ICommentRateRepository repository, IUnitOfWork unitOfWork, ICommentService commentService)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.commentService = commentService;
        }
        public void Add(CommentRateServiceModel item)
        {
            var rate = Mapper.Map<CommentRate>(item);
            repository.Add(rate);
            unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public CommentRateServiceModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CommentRateServiceModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(CommentRateServiceModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}