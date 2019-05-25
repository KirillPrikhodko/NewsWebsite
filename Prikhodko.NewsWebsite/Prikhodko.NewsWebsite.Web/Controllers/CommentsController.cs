using Prikhodko.NewsWebsite.Service.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IUserService userService;
        private readonly IService<CommentServiceModel> commentService;

        public CommentsController(IUserService userService, IService<CommentServiceModel> commentService)
        {
            this.userService = userService;
            this.commentService = commentService;
        }
    }
}