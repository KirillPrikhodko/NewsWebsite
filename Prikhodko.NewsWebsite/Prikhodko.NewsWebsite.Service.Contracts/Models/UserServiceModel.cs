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

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value.Date; }
        }

        public string Country { get; set; }

        public IList<int> CommentRates { get; set; }
    }
}