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
        private readonly IService<TagViewModel> tagService;

        public TagsController(IService<TagViewModel> tagService)
        {
            this.tagService = tagService;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            var tags = tagService.GetAll().Select(x => x.Name).ToList();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }
    }
}