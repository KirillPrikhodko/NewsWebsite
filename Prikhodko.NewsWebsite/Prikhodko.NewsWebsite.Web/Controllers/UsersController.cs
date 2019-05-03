using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult GetUserBar()
        {
            var model = userService.FindById(User.Identity.GetUserId());
            if (model != null)
            {
                return PartialView("_UserBarPartial", model);
            }
            return new EmptyResult();
        }
    }
}