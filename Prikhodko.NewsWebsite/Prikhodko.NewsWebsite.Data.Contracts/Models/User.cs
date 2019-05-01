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
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostRate> PostRates { get; set; }
        public virtual int AvgRate { get; set; } //An average rate of all user's posts
    }
}