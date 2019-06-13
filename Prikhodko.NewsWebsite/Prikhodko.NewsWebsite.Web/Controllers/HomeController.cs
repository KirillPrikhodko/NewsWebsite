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
        private readonly IPostService postService;
        private readonly ITagService tagService;

        public HomeController(ICategoryService categoryService, IPostService postService, ITagService tagService)
        {
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
            return PartialView("_GetPosts", model);
        }

        public ActionResult GetBestPosts(int? page)
        {
            int pageNumber = page ?? 1;
            var model = postService.GetBest(minimumRate: 4).Select(x => Mapper.Map<PostViewModel>(x)).ToPagedList(pageNumber, 10);
            return PartialView("_GetPosts", model);
        }

        public ActionResult GetTagCloud()
        {
            var model = tagService.GetAll() ?? new List<TagServiceModel>();
            return PartialView("_GetTagCloud", model);
        }
    }
}