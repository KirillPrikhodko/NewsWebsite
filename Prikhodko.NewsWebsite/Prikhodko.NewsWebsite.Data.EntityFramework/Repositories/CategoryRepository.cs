using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(Category item)
        {
            if(item != null)
            {
                dbContext.Categories.Add(item);
            }
        }

        public void Delete(int id)
        {
            if (id <= 0)
            {
                return;
            }
            var category = dbContext.Categories.Find(id);
            if (category != null)
            {
                dbContext.Categories.Remove(category);
            }
        }

        public Category Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var category = dbContext.Categories.Find(id);
            return category;
        }

        public IEnumerable<Category> GetAll()
        {
            var categories = dbContext.Categories.ToList();
            return categories;
        }

        public void Update(Category item)
        {
            if (item == null)
            {
                return;
            }

            var categoryToUpdate = dbContext.Categories.Find(item.Id);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = item.Name; //TODO: think of a common interface that will clone (maybe use ICloneable or reflection?)
            }
        }
    }
}