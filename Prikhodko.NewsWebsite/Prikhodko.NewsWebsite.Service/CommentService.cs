using System.Collections.Generic;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class CommentService : IService<CommentServiceModel>
    {
        private readonly IRepository<Comment> repository;
        private readonly IUnitOfWork unitOfWork;

        public CommentService(IRepository<Comment> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void Add(CommentServiceModel item)
        {
            if (item == null)
            {
                return;
            }
            var comment = Mapper.Map<Comment>(item);
            repository.Add(comment);
            unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public CommentServiceModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CommentServiceModel> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public void Update(CommentServiceModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}