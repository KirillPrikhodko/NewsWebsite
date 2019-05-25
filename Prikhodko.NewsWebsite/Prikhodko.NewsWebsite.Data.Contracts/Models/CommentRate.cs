using System.ComponentModel.DataAnnotations.Schema;

namespace Prikhodko.NewsWebsite.Data.Contracts.Models
{
    public class CommentRate
    {
        public virtual int Id { get; set; }
        public virtual int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }
        public virtual string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        public virtual bool Value { get; set; }
    }
}