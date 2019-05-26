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
        private readonly IService<CategoryServiceModel> categoryService;
        private readonly IPostService postService;

        public HomeController(IService<CategoryServiceModel> categoryService, IPostService postService)
        {
            this.categoryService = categoryService;
            this.postService = postService;
        }
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Fresh()
        //{
        //    return View();
        //}

        public ActionResult Fresh(/*int amount*/)
        {
            int amount = 10; //TODO: add polzunok to webpage so user will select the amount
            if (amount <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = postService.GetFresh(amount).Select(x => Mapper.Map<PostViewModel>(x));
            return View(model);
        }

        //public ActionResult Best()
        //{
        //    return View();
        //}

        public ActionResult Best(/*double minimumRate, int amount*/)
        {
            double minimumRate = 4; //TODO: add polzunok to webpage so user will select the minumumRate
            int amount = 10; //TODO: add polzunok to webpage so user will select the amount
            if (minimumRate < 0 || double.IsInfinity(minimumRate) || double.IsNaN(minimumRate) || amount <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = postService.GetBest(minimumRate, amount).Select(x => Mapper.Map<PostViewModel>(x));
            return View(model);
        }

        public ActionResult TagCloud()
        {
            return View();
        }
    }
}