using System.Collections.Generic;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public CommentService(ICommentRepository repository, IUnitOfWork unitOfWork)
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
            item.Id = comment.Id;
        }

        public void Delete(int id)
        {
            if(id <= 0)
            {
                return;
            }
            repository.Delete(id);
            unitOfWork.SaveChanges();
        }

        public CommentServiceModel Get(int id)
        {
            var result = Mapper.Map<CommentServiceModel>(repository.Get(id));
            return result;
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