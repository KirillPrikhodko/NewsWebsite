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

        public ActionResult Details(int id)
        {
            var serviceModel = postService.Get(id);
            var model = Mapper.Map<PostViewModel>(serviceModel);
            if (model == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway);
            }

            foreach (var rate in serviceModel.Rates)
            {
                if (rate.Author.Id == HttpContext.User.Identity.GetUserId())
                {
                    model.RatedByCurrentUser = true;
                    model.CurrentUserRateValue = rate.Value;
                    break;
                }
            }

            ViewBag.Author = userService.FindById(model.AuthorId);
            return View(model);
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
            if (serviceModel.Id > 0) //postService should add Id to service model after the post is added to DB
            {
                return RedirectToAction("Details", "Post", new {Id = serviceModel.Id});
            }

            return RedirectToAction("Index", "Home");
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