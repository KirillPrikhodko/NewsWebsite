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
        private readonly IService<CategoryServiceModel> categoryService;

        public HomeController(IService<CategoryServiceModel> categoryService)
        {
            this.categoryService = categoryService;
        }
        public ActionResult Index()
        {
            ViewBag.Categories = categoryService.GetAll();
            return View();
        }

        public ActionResult Fresh()
        {
            return PartialView();
        }

        public ActionResult Best()
        {
            return PartialView();
        }

        public ActionResult TagCloud()
        {
            return PartialView();
        }

        public IEnumerable<CategoryServiceModel> GetCategories()
        {
            var result = categoryService.GetAll();
            return result;
        }
    }
}