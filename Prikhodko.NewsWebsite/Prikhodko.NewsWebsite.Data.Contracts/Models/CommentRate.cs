using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Data.Contracts.Models
{
    public class CommentRate
    {
        public int Id { get; set; }
        public virtual int CommentId { get; set; }

        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }
        public virtual string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }
        public virtual bool Value { get; set; } //true for +, false for -
    }
}
