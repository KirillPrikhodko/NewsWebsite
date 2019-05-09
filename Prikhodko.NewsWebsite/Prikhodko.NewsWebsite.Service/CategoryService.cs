using System.Collections.Generic;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class CategoryService : IService<CategoryServiceModel>
    {
        private readonly IRepository<Category> repository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public void Add(CategoryServiceModel item)
        {
            Category category = Mapper.Map<Category>(item); //TODO: add mapping profiles
            repository.Add(category);
            unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            unitOfWork.SaveChanges();
        }

        public CategoryServiceModel Get(int id)
        {
            var category = repository.Get(id);
            var result = Mapper.Map<CategoryServiceModel>(category);
            return result;
        }

        public IEnumerable<CategoryServiceModel> GetAll()
        {
            var categories = repository.GetAll();
            foreach (var category in categories)
            {
                yield return Mapper.Map<CategoryServiceModel>(category); //TODO: check whether this is correct
            }
        }

        public void Update(CategoryServiceModel item)
        {
            var category = Mapper.Map<Category>(item);
            repository.Update(category);
            unitOfWork.SaveChanges();
        }
    }
}