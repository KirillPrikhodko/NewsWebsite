using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public void Add(CategoryServiceModel item)
        {
            if(item == null)
            {
                return;
            }
            Category category = Mapper.Map<Category>(item);
            repository.Add(category);
            unitOfWork.SaveChanges();
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

        public CategoryServiceModel Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            var category = repository.Get(id);

            if(category == null)
            {
                return null;
            }

            var result = Mapper.Map<CategoryServiceModel>(category);
            return result;
        }

        public IEnumerable<CategoryServiceModel> GetAll()
        {
            var categories = repository.GetAll().Select(x => Mapper.Map<CategoryServiceModel>(x));
            return categories.ToList();
        }

        public void Update(CategoryServiceModel item)
        {
            if(item == null)
            {
                return;
            }
            var category = Mapper.Map<Category>(item);
            repository.Update(category);
            unitOfWork.SaveChanges();
        }
    }
}