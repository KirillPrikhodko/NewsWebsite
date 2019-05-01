using System.Collections.Generic;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class CommentViewModel
    {

        public virtual UserViewModel Author { get; set; }
        public virtual string Content { get; set; } //TODO: like in posts, Content entity is probably needed (comments may include, apart from plain text, images, gifs, etc.
        public virtual IEnumerable<LikeViewModel> Likes { get; set; }
    }
}