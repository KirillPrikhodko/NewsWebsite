using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PagedList;
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

        public async Task<ActionResult> Details(int id)
        {
            var postServiceModel = postService.Get(id);
            if (postServiceModel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var postViewModel = Mapper.Map<PostViewModel>(postServiceModel);

            foreach (var rate in postServiceModel.Rates)
            {
                if (rate.Author.Id == HttpContext.User.Identity.GetUserId()) //if the service model has been rated by current user
                {
                    postViewModel.RatedByCurrentUser = true; //this information is stored in postViewModel
                    postViewModel.CurrentUserRateValue = rate.Value; //and later in View it will display user's rate and disable rating mechanism
                    break;
                }
            }

            //TODO: delete this because I transferred it to mapping
            //if (postViewModel.Comments == null)
            //{
            //    postViewModel.Comments = new List<CommentServiceModel>();
            //}
            ViewBag.Author = await userService.FindByIdAsync(postViewModel.AuthorId);
            ViewBag.CurrentUser = await userService.FindByIdAsync(HttpContext.User.Identity.GetUserId());
            return View(postViewModel);
        }

        [Authorize (Roles = "Admin,Writer")]
        [Authorize ]
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
                return RedirectToAction("Details", "Post", new { serviceModel.Id });
            }

            return RedirectToAction("Index", "Home");
        }


        [Authorize (Roles = "Admin,Writer")]
        public ActionResult Edit(int id)
        {
            var currentPost = postService.Get(id);

            if (currentPost == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var model = Mapper.Map<EditPostViewModel>(currentPost);
            if (currentPost.AuthorId == HttpContext.User.Identity.GetUserId() ||
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

            if (currentPost == null)
            {
                return new HttpStatusCodeResult(404);
            }

            var post = Mapper.Map<PostServiceModel>(model);
            if (currentPost.AuthorName == HttpContext.User.Identity.Name ||
                HttpContext.User.IsInRole("Admin")) //only admin or author can edit post
            {
                post.Created = DateTime.Now;
                postService.Update(post);
                return RedirectToAction("Details", "Users", new { id = currentPost.AuthorId });
            }

            return new HttpStatusCodeResult(403);
        }

        [Authorize (Roles = "Admin,Writer")]
        public ActionResult Delete(int id, string userId)
        {
            var toDelete = postService.Get(id);
            if (toDelete.AuthorId != HttpContext.User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            postService.Delete(id);
            return RedirectToAction("Details", "Users", new {id = userId});
        }

        [Authorize(Roles = "Admin,Reader,Writer")]
        public async Task<ActionResult> AddRate(double rate, int postId)
        {
            PostRateServiceModel postRate = new PostRateServiceModel() { Author = await userService.FindByIdAsync(HttpContext.User.Identity.GetUserId()), PostId = postId, Value = rate };
            rateService.Add(postRate);
            return new HttpStatusCodeResult(200);
        }

        public async Task<ActionResult> GetUserPosts(string id, string sortOrder, string searchString, int? page)
        {
            #region Paging
            int pageNumber = page ?? 1;
            var user = await userService.FindByIdAsync(id);
            var posts = user.Posts.Select(x => Mapper.Map<PostViewModel>(x));
            #endregion
            #region Search
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(x => x.Title.Contains(searchString)
                                       || x.Title.Contains(searchString));
            }
            #endregion
            #region Sort
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.CategorySortParm = sortOrder == "category" ? "category_desc" : "category";
            ViewBag.AvgRateSortParm = sortOrder == "avgrate" ? "avgrate_desc" : "avgrate";
            ViewBag.NumberOfCommentsSortParm = sortOrder == "numberofcomments" ? "numberofcomments_desc" : "numberofcomments";
            ViewBag.CreatedSortParm = sortOrder == "created" ? "created_desc" : "created";
            switch (sortOrder)
            {
                case "category":
                    posts = posts.OrderBy(x => x.Category);
                    break;
                case "category_desc":
                    posts = posts.OrderByDescending(x => x.Category);
                    break;
                case "name":
                    posts = posts.OrderBy(x => x.Title);
                    break;
                case "name_desc":
                    posts = posts.OrderByDescending(x => x.Title);
                    break;
                case "avgrate":
                    posts = posts.OrderBy(x => x.AvgRate);
                    break;
                case "avgrate_desc":
                    posts = posts.OrderByDescending(x => x.AvgRate);
                    break;
                case "numberofcomments":
                    posts = posts.OrderBy(x => x.Comments.Count);
                    break;
                case "numberofcomments_desc":
                    posts = posts.OrderByDescending(x => x.Comments.Count);
                    break;
                case "created":
                    posts = posts.OrderBy(x => x.Created);
                    break;
                case "created_desc":
                    posts = posts.OrderByDescending(x => x.Created);
                    break;
                default:
                    posts = posts.OrderBy(x => x.Title);
                    break;
            }
            #endregion
            ViewBag.LetEditAndDelete = HttpContext.User.IsInRole("Admin") || HttpContext.User.Identity.GetUserId() == id;   //if admin requests posts table or user requests his own posts
            return PartialView("_UserPostsPartial", posts.ToPagedList(pageNumber, 10));                                                            //he should be able to edit or delete posts
        }
    }
}