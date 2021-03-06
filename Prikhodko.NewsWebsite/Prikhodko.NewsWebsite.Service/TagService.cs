﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Service
{
    public class TagService : ITagService
    {
        private readonly ITagRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public TagService(ITagRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<TagServiceModel> GetAll()
        {
            var tags = repository.GetAll();
            var result = tags.Select(x => Mapper.Map<TagServiceModel>(x));
            return result.ToList();
        }
    }
}