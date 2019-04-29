using System.Collections.Generic;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class PostService : IService<PostViewModel>
    {
        private readonly IRepository<Post> postRepository;
        private readonly IUnitOfWork unitOfWork;

        public PostService(IRepository<Post> postRepository, IUnitOfWork unitOfWork)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Add(PostViewModel item)
        {
            var post = Mapper.Map<Post>(item);
            postRepository.Add(post);
            unitOfWork.SaveChanges();
        }

        public void Delete(int id)
        {
            postRepository.Delete(id);
            unitOfWork.SaveChanges();
        }

        public PostViewModel Get(int id)
        {
            var post = postRepository.Get(id);
            var result = Mapper.Map<PostViewModel>(post);
            return result;
        }

        public IEnumerable<PostViewModel> GetAll()
        {
            var posts = postRepository.GetAll();
            var result = new List<PostViewModel>();
            foreach (var post in posts)
            {
                result.Add(Mapper.Map<PostViewModel>(post));
            }
            return result;
        }

        public void Update(PostViewModel item)
        {
            var post = Mapper.Map<Post>(item);
            postRepository.Update(post);
            unitOfWork.SaveChanges();
        }
    }
}