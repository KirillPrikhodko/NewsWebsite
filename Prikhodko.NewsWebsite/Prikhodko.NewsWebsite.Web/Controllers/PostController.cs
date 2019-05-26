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
        private readonly IPostService postService;
        private readonly IService<CategoryServiceModel> categoryService;
        private readonly IService<PostRateServiceModel> rateService;
        private readonly IUserService userService;

        public PostController(IPostService postService, IService<CategoryServiceModel> categoryService, IService<PostRateServiceModel> rateService, IUserService userService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
            this.userService = userService;
            this.rateService = rateService;
        }

        public ActionResult Details(int id)
        {
            var postServiceModel = postService.Get(id);
            var postViewModel = Mapper.Map<PostViewModel>(postServiceModel);
            if (postViewModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            foreach (var rate in postServiceModel.Rates)
            {
                if (rate.Author.Id == HttpContext.User.Identity.GetUserId())
                {
                    postViewModel.RatedByCurrentUser = true;
                    postViewModel.CurrentUserRateValue = rate.Value;
                    break;
                }
            }

            if (postViewModel.Comments == null)
            {
                postViewModel.Comments = new List<CommentServiceModel>();
            }
            ViewBag.UserAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            ViewBag.Author = userService.FindById(postViewModel.AuthorId);
            ViewBag.CurrentUser = userService.FindById(HttpContext.User.Identity.GetUserId());
            return View(postViewModel);
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
            model.Created = DateTime.Now;
            model.AuthorId = HttpContext.User.Identity.GetUserId();
            var serviceModel = Mapper.Map<PostServiceModel>(model);
            postService.Add(serviceModel);
            if (serviceModel.Id > 0) //postService should add Id to service model after the post is added to DB
            {
                return RedirectToAction("Details", "Post", new { Id = serviceModel.Id });
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

        public ActionResult AddRate(double rate, int postId)
        {
            PostRateServiceModel postRate = new PostRateServiceModel() { Author = userService.FindById(HttpContext.User.Identity.GetUserId()), PostId = postId, Value = rate };
            rateService.Add(postRate);
            return new EmptyResult();
        }
    }
}