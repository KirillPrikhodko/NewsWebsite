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
        private readonly ICategoryService categoryService;
        private readonly IPostRateService rateService;
        private readonly IUserService userService;

        public PostController(IPostService postService, ICategoryService categoryService, IPostRateService rateService, IUserService userService)
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

        [Authorize (Roles = "Admin,Writer")]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryService.GetAll().Select(x => x.Name));
            return View();
        }

        [Authorize (Roles = "Admin,Writer")]
        [HttpPost]
        public ActionResult Create(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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


        [Authorize (Roles = "Admin,Writer")]
        public ActionResult Edit(int id)
        {
            var currentPost = postService.Get(id);
            var model = Mapper.Map<EditPostViewModel>(currentPost);
            if (model == null)
            {
                return new HttpStatusCodeResult(404);
            }
            if (currentPost.AuthorName == HttpContext.User.Identity.Name ||
                HttpContext.User.IsInRole("Admin")) //only admin or author can edit post
            {
                ViewBag.Categories = new SelectList(categoryService.GetAll().Select(x => x.Name));
                return View(model);
            }
            return new HttpStatusCodeResult(400);
        }

        [Authorize (Roles = "Admin,Writer")]
        [HttpPost]
        public ActionResult Edit(EditPostViewModel model)
        {
            var currentPost = postService.Get(model.Id);
            var post = Mapper.Map<PostServiceModel>(model);
            if (currentPost.AuthorName == HttpContext.User.Identity.Name ||
                HttpContext.User.IsInRole("Admin")) //only admin or author can edit post
            {
                postService.Update(post);
                return RedirectToAction("Details", "Users", new { name = currentPost.AuthorName });
            }

            return new HttpStatusCodeResult(403);
        }

        [Authorize (Roles = "Admin,Writer")]
        public ActionResult Delete(int id, string username)
        {
            postService.Delete(id);
            return RedirectToAction("Details", "Users", new  {name = username });
        }

        [Authorize(Roles = "Admin,Reader,Writer")]
        public ActionResult AddRate(double rate, int postId)
        {
            PostRateServiceModel postRate = new PostRateServiceModel() { Author = userService.FindById(HttpContext.User.Identity.GetUserId()), PostId = postId, Value = rate };
            rateService.Add(postRate);
            return new EmptyResult();
        }

        public ActionResult GetUserPosts(string name)
        {
            var posts = userService.FindByName(name).Posts.Select(x => Mapper.Map<PostViewModel>(x)).ToList();
            ViewBag.LetEditAndDelete = HttpContext.User.IsInRole("Admin") || HttpContext.User.Identity.Name == name;   //if admin requests posts table or user requests his own posts
            return PartialView("_UserPostsPartial", posts);                                                            //he should be able to edit or delete posts
        }
    }
}