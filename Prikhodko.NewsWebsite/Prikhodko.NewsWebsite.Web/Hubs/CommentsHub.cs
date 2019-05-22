using System;
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
        private readonly IService<CommentServiceModel> commentService;

        public CommentsHub(IService<CommentServiceModel> commentService)
        {
            this.commentService = commentService;
        }
        public void AddComment(string content, int postId)
        {
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
    }
}