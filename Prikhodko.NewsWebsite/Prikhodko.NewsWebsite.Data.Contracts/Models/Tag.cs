using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prikhodko.NewsWebsite.Data.Contracts.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Post> Posts { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
