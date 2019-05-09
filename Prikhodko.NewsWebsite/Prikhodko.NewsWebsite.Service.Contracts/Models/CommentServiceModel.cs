using System.Collections.Generic;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class CommentServiceModel
    {

        public virtual UserServiceModel Author { get; set; }
        public virtual string Content { get; set; } //TODO: like in posts, Content entity is probably needed (comments may include, apart from plain text, images, gifs, etc.
        public virtual IEnumerable<LikeServiceModel> Likes { get; set; }
    }
}