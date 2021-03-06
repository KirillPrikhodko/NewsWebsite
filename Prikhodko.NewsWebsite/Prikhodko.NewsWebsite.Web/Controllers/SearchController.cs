﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using Prikhodko.NewsWebsite.Service.Contracts.Interfaces;
using Prikhodko.NewsWebsite.Service.Contracts.Models;
using Prikhodko.NewsWebsite.Web.Models;

namespace Prikhodko.NewsWebsite.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPostService postService;

        public SearchController(IPostService postService)
        {
            this.postService = postService;
        }

        public ActionResult Index()
        {
            return View("Tag");
        }

        [HttpGet]
        public ActionResult Tag(string tagText, int? page)
        {
            ViewBag.Tag = tagText;
            var tagModel = new TagServiceModel(){Name = tagText };
            int pageNumber = page ?? 1;
            var posts = postService.GetByTag(tagModel).Select(x => Mapper.Map<PostViewModel>(x));
            return View(posts.ToPagedList(pageNumber, 10));
        }
    }
}