using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;
using Prikhodko.NewsWebsite.Web.Models;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IService<PostViewModel> postService;
        private readonly IService<CategoryViewModel> categoryService;

        public PostController(IService<PostViewModel> postService, IService<CategoryViewModel> categoryService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
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
        public ActionResult Create(PostViewModel model) //TODO: validate model for smth
        {
            postService.Add(model);
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
        public ActionResult Edit(PostViewModel model)
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