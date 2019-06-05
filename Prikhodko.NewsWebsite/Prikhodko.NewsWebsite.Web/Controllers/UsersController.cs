using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = userService.GetAll().Where(x => x.ApplicationIdentityUser.UserName.ToLower() != "admin").Where(x => x.ApplicationIdentityUser.IsEnabled == true).ToList(); //admin is not listed
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (id.ToLower() == HttpContext.User.Identity.GetUserId().ToLower())
                {
                    return RedirectToAction("Index", "Manage");
                }
            }
            var model = userService.FindById(id);
            return View(model);
        }

        public ActionResult EditName(string value)
        {
            userService.EditName(HttpContext.User.Identity.GetUserId(), value);
            return new HttpStatusCodeResult(200);
        }

        public ActionResult EditCountry(string value)
        {
            userService.EditCountry(HttpContext.User.Identity.GetUserId(), value);
            return new HttpStatusCodeResult(200);
        }

        public ActionResult EditDateOfBirth(DateTime value)
        {
            userService.EditDateOfBirth(HttpContext.User.Identity.GetUserId(), value);
            return new HttpStatusCodeResult(200);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddWriterPermissions(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userService.AddRole(id, "Writer");
            return new HttpStatusCodeResult(200);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveWriterPermissions(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userService.RemoveRole(id, "Writer");
            return new HttpStatusCodeResult(200);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddAdminPermissions(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userService.AddRole(id, "Admin");
            return new HttpStatusCodeResult(200);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RemoveAdminPermissions(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userService.RemoveRole(id, "Admin");
            return new HttpStatusCodeResult(200);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Block(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userService.AddRole(id, "Blocked");
            return new HttpStatusCodeResult(200);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Unblock(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userService.RemoveRole(id, "Blocked");
            return new HttpStatusCodeResult(200);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            userService.Delete(id);
            return new HttpStatusCodeResult(200);
        }
    }
}