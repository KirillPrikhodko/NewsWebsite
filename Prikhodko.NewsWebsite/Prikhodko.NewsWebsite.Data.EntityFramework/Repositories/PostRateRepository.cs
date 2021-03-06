﻿using System;
using System.Collections.Generic;
using Prikhodko.NewsWebsite.Data.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Data.Contracts.Models;

namespace Prikhodko.NewsWebsite.Data.EntityFramework.Repositories
{
    public class PostRateRepository : IPostRateRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPostRepository postRepository;

        public PostRateRepository(ApplicationDbContext dbContext, IPostRepository postRepository)
        {
            this.dbContext = dbContext;
            this.postRepository = postRepository;
        }
        public void Add(PostRate item)
        {
            if (item == null && item.Value <= 0)
            {
                return;
            }
            item.Author = dbContext.Users.Find(item.Author.Id).User;
            dbContext.PostRates.Add(item);
            var updatedPost = dbContext.Posts.Find(item.PostId);
            updatedPost.AvgRate = postRepository.GetAvgPostRate(updatedPost);
        }
    }
}