namespace Prikhodko.NewsWebsite.Service.Contracts.Models
{
    public class CommentRateServiceModel
    {
        public virtual string AuthorId { get; set; }
        public virtual int CommentId { get; set; }
        public virtual bool Value { get; set; }
    }
}