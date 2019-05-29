﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public ActionResult Details(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (name.ToLower() == HttpContext.User.Identity.Name.ToLower())
            {
                return RedirectToAction("Index", "Manage");
            }

            var model = userService.FindByName(name);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(string name)
        {
            throw new NotImplementedException();
        }
    }
}