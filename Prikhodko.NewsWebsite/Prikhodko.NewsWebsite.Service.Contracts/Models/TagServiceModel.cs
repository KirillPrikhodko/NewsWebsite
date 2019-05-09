using System.Collections.Generic;
using Newtonsoft.Json;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class TagServiceModel
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        public virtual IEnumerable<PostServiceModel> Posts { get; set; }
    }
}