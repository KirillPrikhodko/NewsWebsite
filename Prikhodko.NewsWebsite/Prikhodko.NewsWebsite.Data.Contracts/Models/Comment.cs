using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Data.Contracts.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public virtual User Author { get; set; }
        public virtual string Content { get; set; } //TODO: like in posts, Content entity is probably needed (comments may include, apart from plain text, images, gifs, etc.
        public virtual IEnumerable<Like> Likes { get; set; }
    }
}
