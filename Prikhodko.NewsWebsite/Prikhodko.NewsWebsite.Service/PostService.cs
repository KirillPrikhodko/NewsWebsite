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

        public PostService(IPostRepository postRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
            this.tagRepository = tagRepository;
        }
        public void Add(PostServiceModel item)
        {
            if(item == null)
            {
                return;
            }
            var post = Mapper.Map<Post>(item);
            postRepository.Add(post);
            unitOfWork.SaveChanges();
            if (post.Id > 0)
            {
                item.Id = post.Id; //serviceModel Id is later used by controller for redirection to 'Details' page
            }
        }

        public void Delete(int id)
        {
            if(id <= 0)
            {
                return;
            }
            postRepository.Delete(id);
            unitOfWork.SaveChanges();
        }

        public PostServiceModel Get(int id)
        {
            if (id <= 0)
            {
                return null;
            }
            var post = postRepository.Get(id);
            var result = Mapper.Map<PostServiceModel>(post);
            return result;
        }

        public IEnumerable<PostServiceModel> GetAll()
        {
            var result = postRepository.GetAll().Select(x => Mapper.Map<PostServiceModel>(x));
            return result.ToList();
        }

        public IEnumerable<PostServiceModel> GetBest(double minimumRate)
        {
            if (minimumRate < 0 || double.IsInfinity(minimumRate) || double.IsNaN(minimumRate))
            {
                return null;
            }

            var result = postRepository.GetBest(minimumRate).Select(x => Mapper.Map<PostServiceModel>(x));
            return result.ToList();
        }

        public IEnumerable<PostServiceModel> GetByTag(TagServiceModel tagModel)
        {
            if(tagModel == null)
            {
                return null;
            }
            var tag = tagRepository.Ensure(Mapper.Map<Tag>(tagModel));
            var posts = postRepository.GetByTag(tag);
            var result = posts.Select(x => Mapper.Map<PostServiceModel>(x));
            return result.ToList();
        }

        public IEnumerable<PostServiceModel> GetFresh()
        {
            var result = postRepository.GetFresh().Select(x => Mapper.Map<PostServiceModel>(x));
            return result.ToList();
        }

        public void Update(PostServiceModel item)
        {
            if(item == null)
            {
                return;
            }
            var post = Mapper.Map<Post>(item);
            postRepository.Update(post);
            unitOfWork.SaveChanges();
        }
    }
}