using System.Collections.Generic;
using Newtonsoft.Json;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class TagViewModel
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        public virtual IEnumerable<PostViewModel> Posts { get; set; }
    }
}