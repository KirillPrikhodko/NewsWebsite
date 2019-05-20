using System.Collections.Generic;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class CommentServiceModel
    {
        public virtual int Id { get; set; }
        public virtual string AuthorId { get; set; }
        public virtual string AuthorName { get; set; }
        public virtual string Content { get; set; }
        public virtual int Rating { get; set; }
        public virtual int PostId { get; set; }
    }
}