using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Data.Contracts.Models
{
    public class User
    {
        public string Id { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationIdentityUser ApplicationIdentityUser { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public virtual IList<Comment> Comments { get; set; }
        public virtual IList<CommentRate> CommentRates { get; set; }
        public virtual IList<PostRate> PostRates { get; set; }
        public virtual int AvgRate { get; set; } //An average rate of all user's posts
        public string ProfileImagePath { get; set; }
        public string Name { get; set; }
        
        public DateTime? DateOfBirth { get; set; }

        public string Country { get; set; }
    }
}