using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;
using Prikhodko.NewsWebsite.Web.Models;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;
        private readonly ITagService tagService;

        public HomeController(ICategoryService categoryService, IPostService postService, ITagService tagService)
        {
            this.categoryService = categoryService;
            this.postService = postService;
            this.tagService = tagService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangeCulture(string culture, string returnUrl)
        {
            HttpContext.Response.Cookies.Add(new HttpCookie("culture", culture));
            return Redirect(returnUrl);
        }

        public ActionResult GetFreshPosts(int? page)
        {
            int pageNumber = page ?? 1;
            var model = postService.GetFresh().Select(x => Mapper.Map<PostViewModel>(x)).ToPagedList(pageNumber, 10);
            return PartialView("_GetFreshPosts", model);
        }

        public ActionResult GetBestPosts(int? page)
        {
            int pageNumber = page ?? 1;
            double minimumRate = 4;
            int amount = 10;
            if (minimumRate < 0 || double.IsInfinity(minimumRate) || double.IsNaN(minimumRate) || amount <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = postService.GetBest(minimumRate).Select(x => Mapper.Map<PostViewModel>(x)).ToPagedList(pageNumber, 10);
            return PartialView("_GetBestPosts", model);
        }

        public ActionResult GetTagCloud()
        {
            var model = tagService.GetAll() ?? new List<TagServiceModel>();
            return PartialView("_GetTagCloud", model);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}