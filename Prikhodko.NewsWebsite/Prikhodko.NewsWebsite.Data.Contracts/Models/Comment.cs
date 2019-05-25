﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Data.Contracts.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public virtual string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }
        public virtual int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        public virtual string Content { get; set; }
        public virtual IList<CommentRate> Rates { get; set; }
    }
}
