using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prikhodko.NewsWebsite.Data.Contracts.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } //TODO: provide automatic inclusion of categories in database (likely via Seed method in migration
    }
}
