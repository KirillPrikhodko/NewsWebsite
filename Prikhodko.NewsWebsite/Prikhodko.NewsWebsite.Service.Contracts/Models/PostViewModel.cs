using System.Collections.Generic;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class PostViewModel
    {
        public virtual UserViewModel Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual CategoryViewModel Category { get; set; }
        public virtual IEnumerable<string> Tags { get; set; }
        public virtual string Content { get; set; }
        public int AvgRate { get; set; }
    }
}