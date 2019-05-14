using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class PostRateServiceModel
    {
        public virtual UserServiceModel Author { get; set; }
        public virtual int PostId { get; set; }
        public double Value { get; set; } //TODO: value should be between 1 and 5
    }
}