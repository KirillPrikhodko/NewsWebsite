using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
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
            int amount = 10;
            ViewBag.Fresh = postService.GetFresh(amount).Select(x => Mapper.Map<PostViewModel>(x));
            ViewBag.Best = postService.GetBest(4.5, 10).Select(x => Mapper.Map<PostViewModel>(x));
            return View();
        }

        public ActionResult GetFreshPosts()
        {
            int amount = 10;
            if (amount <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = postService.GetFresh(amount).Select(x => Mapper.Map<PostViewModel>(x));
            return PartialView("_GetFreshPosts", model);
        }

        public ActionResult GetBestPosts()
        {
            double minimumRate = 4;
            int amount = 10;
            if (minimumRate < 0 || double.IsInfinity(minimumRate) || double.IsNaN(minimumRate) || amount <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = postService.GetBest(minimumRate, amount).Select(x => Mapper.Map<PostViewModel>(x));
            return PartialView("_GetBestPosts", model);
        }

        public ActionResult GetTagCloud()
        {
            var model = tagService.GetAmount(10) ?? new List<TagServiceModel>();
            return PartialView("_GetTagCloud", model);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}