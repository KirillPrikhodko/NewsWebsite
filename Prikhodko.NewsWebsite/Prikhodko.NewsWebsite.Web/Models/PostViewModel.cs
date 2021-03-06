﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Prikhodko.NewsWebsite.Service.Contracts.Models;
using Prikhodko.NewsWebsite.Web.Validation;

namespace Prikhodko.NewsWebsite.Web.Models
{
    [Validator(typeof(PostValidator))]
    public class PostViewModel
    {
        public int Id { get; set; }
        public virtual string AuthorId { get; set; }
        public virtual string AuthorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual string Category { get; set; }
        public virtual IEnumerable<string> Tags { get; set; }

        [AllowHtml]
        public virtual string Content { get; set; }
        public double? AvgRate { get; set; }

        public bool RatedByCurrentUser { get; set; } //this will determine whether the stars should be enabled on post/details page
        public double CurrentUserRateValue { get; set; }
        public IList<CommentServiceModel> Comments { get; set; }

        public virtual DateTime Created { get; set; }
    }
}