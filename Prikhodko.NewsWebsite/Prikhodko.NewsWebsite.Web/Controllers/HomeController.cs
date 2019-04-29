using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<CategoryViewModel> categoryService;

        public HomeController(IService<CategoryViewModel> categoryService)
        {
            this.categoryService = categoryService;
        }
        public ActionResult Index()
        {
            ViewBag.Categories = categoryService.GetAll();
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Best()
        {
            return View();
        }

        public ActionResult TagCloud()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}