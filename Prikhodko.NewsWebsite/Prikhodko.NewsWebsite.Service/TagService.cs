using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class TagService : IService<TagServiceModel>
    {
        private readonly IRepository<Tag> repository;
        private readonly IUnitOfWork unitOfWork;

        public TagService(IRepository<Tag> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public void Add(TagServiceModel item)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public TagServiceModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TagServiceModel> GetAll()
        {
            var tags = repository.GetAll();
            var result = tags.Select(x => Mapper.Map<TagServiceModel>(x)).ToList();
            return result;
        }

        public void Update(TagServiceModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}