using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Data.Contracts.Models
{
    public class PostRate
    {
        public int Id { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public int Value { get; set; } //TODO: value should be between 1 and 5
    }
}
