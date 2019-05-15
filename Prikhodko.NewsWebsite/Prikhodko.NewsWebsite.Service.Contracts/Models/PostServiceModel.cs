using System.Collections.Generic;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class PostServiceModel
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public virtual IEnumerable<string> Tags { get; set; }
        public string Content { get; set; }
        public IList<PostRateServiceModel> Rates { get; set; }
        public IList<CommentServiceModel> Comments { get; set; }
        public double AvgRate { get; set; }
    }
}