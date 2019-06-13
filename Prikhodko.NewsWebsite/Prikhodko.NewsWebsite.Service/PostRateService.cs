using System;
using System.Collections.Generic;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class PostRateService : IPostRateService
    {
        private readonly IPostRateRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public PostRateService(IPostRateRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        public void Add(PostRateServiceModel item)
        {
            if (item != null && item.Value > 0 && item.Value <= 5)
            {
                var postRate = Mapper.Map<PostRate>(item);
                repository.Add(postRate);
            }
            else
            {
                return;
            }
            unitOfWork.SaveChanges();
        }
    }
}