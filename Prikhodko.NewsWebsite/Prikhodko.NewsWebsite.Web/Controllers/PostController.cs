using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;
using Prikhodko.NewsWebsite.Web.Models;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IService<PostServiceModel> postService;
        private readonly IService<CategoryServiceModel> categoryService;
        private readonly IUserService userService;

        public PostController(IService<PostServiceModel> postService, IService<CategoryServiceModel> categoryService, IUserService userService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
            this.userService = userService;
        }
        // GET: Post
        public ActionResult Index() //TODO: I should check if this is actually necessary
        {
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryService.GetAll().Select(x => x.Name));
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); //TODO: should smth else be done here?
            }

            model.AuthorId = HttpContext.User.Identity.GetUserId();
            var serviceModel = Mapper.Map<PostServiceModel>(model);
            postService.Add(serviceModel);
            return View();
        }


        [Authorize]
        public ActionResult Edit(int id)
        {
            var model = postService.Get(id);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(PostServiceModel model)
        {
            postService.Update(model);
            return View();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            postService.Delete(id);
            return RedirectToAction("Index", "Manage");
        }
    }
}