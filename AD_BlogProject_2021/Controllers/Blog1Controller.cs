﻿using AD_BlogProject_2021.Models.EntityFramework;
using AD_BlogProject_2021.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AD_BlogProject_2021.Controllers
{
    public class Blog1Controller : Controller
    {
        MyBlogContext db = new MyBlogContext();
        // GET: Blog1
        public ActionResult Index()
        {
            var model = new BlogViewModel();
            model.Blogs= db.Blogs.Where(b => b.IsActive == true).OrderByDescending(b => b.BlogId).ToList();
            model.Tags = db.Tags.Where(t => t.IsActive== true).ToList();
            return View(model);
        }
        public ActionResult TagFilter(int TagId)
        {
            var model = db.Blogs.Where(u => u.IsActive == true).ToList();
            if (TagId != 0 && model.Any(b => b.Tags.Any(t => t.TagId == TagId)))
            {
                model = db.Blogs.Where(b => b.Tags.Any(t => t.TagId == TagId)).ToList();
            }
            else
            {
                model = db.Blogs.Where(u => u.IsActive == true).ToList();
            }

            return PartialView("_BlogFilter", model);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = db.Blogs.Find(id);

            if (blogs == null)
            {
                return HttpNotFound();
            }
            ViewBag.TagId = db.Tags.Where(t => t.IsActive == true).ToList();
            
            return View(blogs);
            
        }
        public ActionResult About()
        {

            return View();
        }
    }
}