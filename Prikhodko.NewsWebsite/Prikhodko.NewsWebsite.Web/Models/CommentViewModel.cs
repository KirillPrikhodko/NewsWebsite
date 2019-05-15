namespace Prikhodko.NewsWebsite.Web.Models
{
    public class CommentViewModel
    {
        public virtual string AuthorId { get; set; }
        public virtual string Content { get; set; }
        public virtual int LikesCount { get; set; }
    }
}