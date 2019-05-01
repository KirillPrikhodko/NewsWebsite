using System.Collections.Generic;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Data.Contracts.Models
{
    public class User
    {
        public int Id { get; set; }
        public ApplicationIdentityUser ApplicationIdentityUser { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostRate> PostRates { get; set; }
        public int AvgRate { get; set; } //An average rate of all user's posts
    }
}