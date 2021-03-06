﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Web.Hubs
{
    public class CommentsHub : Hub
    {
        private readonly ICommentService commentService;
        private readonly ICommentRateService commentRateService;

        public CommentsHub(ICommentService commentService, ICommentRateService commentRateService)
        {
            this.commentService = commentService;
            this.commentRateService = commentRateService;
        }
        
        [Authorize(Roles="Admin,Reader,Writer")]
        public void Send(string content, int postId)
        {
            if (string.IsNullOrWhiteSpace(content) || postId <= 0)
            {
                return;
            }
            CommentServiceModel model = new CommentServiceModel()
            {
                AuthorId = Context.User.Identity.GetUserId(),
                AuthorName = Context.User.Identity.GetUserName(),
                Content = content,
                PostId = postId
            };
            commentService.Add(model);
            Clients.All.addNewComment(model.AuthorName, model.AuthorId, model.Content, model.Rating, model.Id);
        }
        
        [Authorize(Roles = "Admin,Reader,Writer")]
        public void AddVote(bool value, string stringCommentId)
        {
            string textToReplace = value ? "upvote" : "downvote"; //if the vote is positive, the value will be true => id of the clicked button will be "upvoteXXXX"
            int commentId = Int32.Parse(stringCommentId.Replace(textToReplace, "")); //because html upvote button have id "upvoteXXXX" which is passed to server, I have to do this
            commentRateService.Add(new CommentRateServiceModel()
            {
                AuthorId = Context.User.Identity.GetUserId(),
                CommentId = commentId,
                Value = value
            });
            Clients.All.changeRating(commentId, value);
        }

        [Authorize(Roles = "Admin")]
        public void DeleteComment(string stringId)
        {
            int id = Int32.Parse(stringId.Replace("deletecomment", ""));
            if(id <= 0)
            {
                return;
            }
            commentService.Delete(id);
            Clients.All.deleteComment(id);
        }
    }
}