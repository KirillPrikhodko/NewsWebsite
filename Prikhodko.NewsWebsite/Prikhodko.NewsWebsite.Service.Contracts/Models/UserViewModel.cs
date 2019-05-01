using System.Collections.Generic;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class UserViewModel
    {

        public ApplicationIdentityUser ApplicationIdentityUser { get; set; }
        public ICollection<PostViewModel> Posts { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }
        public ICollection<PostRateViewModel> PostRates { get; set; }
        public int AvgRate { get; set; } //An average rate of all user's posts
    }
}