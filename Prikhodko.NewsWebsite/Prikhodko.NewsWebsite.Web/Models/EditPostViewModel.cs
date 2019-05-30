using System.Collections.Generic;
using System.Web.Mvc;

namespace Prikhodko.NewsWebsite.Web.Models
{
    public class EditPostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual string Category { get; set; }
        public virtual IEnumerable<string> Tags { get; set; }

        [AllowHtml]
        public virtual string Content { get; set; }
    }
}