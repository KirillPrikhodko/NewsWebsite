using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class PostRateViewModel
    {
        public virtual ApplicationIdentityUser Author { get; set; }
        public int Value { get; set; } //TODO: value should be between 1 and 5
    }
}