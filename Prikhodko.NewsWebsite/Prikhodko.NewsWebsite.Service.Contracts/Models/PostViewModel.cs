using System.Collections.Generic;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class PostViewModel
    {
        public virtual ApplicationIdentityUser Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual CategoryViewModel Category { get; set; }
        public virtual IEnumerable<TagViewModel> Tags { get; set; }
        public virtual string Content { get; set; } //TODO: create Content entity (likely needed in order to insert images into posts
        public virtual IEnumerable<PostRateViewModel> Rates { get; set; }
        public int AvgRate { get; set; }
    }
}