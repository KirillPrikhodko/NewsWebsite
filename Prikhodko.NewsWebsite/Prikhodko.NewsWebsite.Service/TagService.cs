using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class TagService : IService<TagViewModel>
    {
        private readonly IRepository<Tag> repository;
        private readonly IUnitOfWork unitOfWork;

        public TagService(IRepository<Tag> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public void Add(TagViewModel item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public TagViewModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TagViewModel> GetAll()
        {
            var tags = repository.GetAll();
            var result = tags.Select(x => Mapper.Map<TagViewModel>(x)).ToList();
            return result;
        }

        public void Update(TagViewModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}