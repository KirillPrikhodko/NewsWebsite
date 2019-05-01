using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IService<CategoryViewModel> categoryService;

        public CategoriesController(IService<CategoryViewModel> categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult GetCategoriesDropdown()
        {
            ViewBag.Categories = categoryService.GetAll();
            return PartialView("_GetCategoriesDropdownPartial");
        }
    }
}