using System;
using System.Collections.Generic;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class UserServiceModel
    {
        public string Id { get; set; }
        public ApplicationIdentityUserServiceModel ApplicationIdentityUser { get; set; }
        public int AvgRate { get; set; } //An average rate of all user's posts

        public string ProfileImagePath { get; set; }

        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Country { get; set; }

        public IList<PostServiceModel> Posts { get; set; }
        public IList<PostRateServiceModel> PostRates { get; set; }
        public IList<CommentServiceModel> Comments { get; set; }
        public IList<CommentRateServiceModel> CommentRates { get; set; }
    }
}