using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagService tagService;

        public TagsController(ITagService tagService)
        {
            this.tagService = tagService;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            var tags = tagService.GetAll().Select(x => x.Name);
            return Json(tags.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}