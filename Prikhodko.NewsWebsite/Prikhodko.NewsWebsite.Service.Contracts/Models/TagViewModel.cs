using System.Collections.Generic;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class TagViewModel
    {
        public int Name { get; set; }
        public virtual IEnumerable<PostViewModel> Posts { get; set; }
    }
}