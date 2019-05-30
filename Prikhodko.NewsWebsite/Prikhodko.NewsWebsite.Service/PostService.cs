using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService userService;

        public PostService(IPostRepository postRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork, IUserService userService)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
            this.userService = userService;
            this.tagRepository = tagRepository;
        }
        public void Add(PostServiceModel item)
        {
            var post = Mapper.Map<Post>(item);
            post.Tags = post.Tags.Distinct().ToList();
            postRepository.Add(post);
            unitOfWork.SaveChanges();
            if (post.Id > 0)
            {
                item.Id = post.Id; //serviceModel Id is later used by controller for redirection to 'Details' page
            }
        }

        public void Delete(int id)
        {
            postRepository.Delete(id);
            unitOfWork.SaveChanges();
        }

        public PostServiceModel Get(int id)
        {
            var post = postRepository.Get(id);
            var result = Mapper.Map<PostServiceModel>(post);
            //if (post != null)
            //{
            //    result.AvgRate = GetAvgPostRate(post.Rates);
            //}
            return result;
        }

        public IEnumerable<PostServiceModel> GetAll()
        {
            var posts = postRepository.GetAll();
            var result = new List<PostServiceModel>();
            foreach (var post in posts)
            {
                result.Add(Mapper.Map<PostServiceModel>(post));
            }
            return result;
        }

        public IEnumerable<PostServiceModel> GetBest(double minimumRate, int amount)
        {
            if (minimumRate < 0 || double.IsInfinity(minimumRate) || double.IsNaN(minimumRate) || amount <= 0)
            {
                return null;
            }

            var result = postRepository.GetBest(minimumRate, amount).Select(x => Mapper.Map<PostServiceModel>(x));
            return result;
        }

        public IEnumerable<PostServiceModel> GetByTag(TagServiceModel tagModel)
        {
            var tag = tagRepository.Ensure(Mapper.Map<Tag>(tagModel));
            var posts = postRepository.GetByTag(tag);
            var result = posts.Select(x => Mapper.Map<PostServiceModel>(x));
            return result;
        }

        public IEnumerable<PostServiceModel> GetFresh(int amount)
        {
            if (amount <= 0)
            {
                return null;
            }

            var result = postRepository.GetFresh(amount).Select(x => Mapper.Map<PostServiceModel>(x));
            return result;
        }

        public void Update(PostServiceModel item)
        {
            var post = Mapper.Map<Post>(item);
            post.Tags = post.Tags.Distinct().ToList();
            postRepository.Update(post);
            unitOfWork.SaveChanges();
        }
    }
}