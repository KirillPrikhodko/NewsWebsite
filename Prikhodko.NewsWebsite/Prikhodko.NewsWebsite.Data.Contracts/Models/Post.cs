﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prikhodko.NewsWebsite.CommonModels;

namespace Prikhodko.NewsWebsite.Data.Contracts.Models
{
    public class Post
    {
        public int Id { get; set; }
        public virtual string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual Category Category { get; set; }
        public virtual IList<Tag> Tags { get; set; }
        public virtual string Content { get; set; }
        public virtual IList<PostRate> Rates { get; set; }
        public virtual double? AvgRate { get; set; }
        public virtual IList<Comment> Comments { get; set; }
        public virtual DateTime Created { get; set; }

        public void Update(Post unit) //TODO: think of a better name
        {
            unit.Title = this.Title;
            unit.Description = this.Description;
            unit.Category = this.Category;
            unit.Tags = this.Tags;
            unit.Content = this.Content;
        }
    }
}
