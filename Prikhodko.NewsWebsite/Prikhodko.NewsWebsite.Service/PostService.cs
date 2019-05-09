using System.Collections.Generic;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class PostService : IService<PostServiceModel>
    {
        private readonly IRepository<Post> repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;

        public PostService(IRepository<Post> repository, IUnitOfWork unitOfWork, IUserService userService)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.userService = userService;
        }
        public void Add(PostServiceModel item)
        {
            var post = Mapper.Map<Post>(item);
            repository.Add(post);
            unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            unitOfWork.SaveChanges();
        }

        public PostServiceModel Get(int id)
        {
            var post = repository.Get(id);
            var result = Mapper.Map<PostServiceModel>(post);
            return result;
        }

        public IEnumerable<PostServiceModel> GetAll()
        {
            var posts = repository.GetAll();
            var result = new List<PostServiceModel>();
            foreach (var post in posts)
            {
                result.Add(Mapper.Map<PostServiceModel>(post));
            }
            return result;
        }

        public void Update(PostServiceModel item)
        {
            var post = Mapper.Map<Post>(item);
            repository.Update(post);
            unitOfWork.SaveChanges();
        }
    }
}