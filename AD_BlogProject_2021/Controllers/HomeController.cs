﻿using AD_BlogProject_2021.Models.EntityFramework;
using AD_BlogProject_2021.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AD_BlogProject_2021.Controllers
{
    public class HomeController : Controller
    {
        MyBlogContext db = new MyBlogContext();
        // GET: Home
        public ActionResult Index()
        {
            var home = db.Homes.Where(h=>h.IsActive==true).ToList();
            return View(home);
        }
        public ActionResult About()
        {
            var model = new AboutViewModel();
            model.Abouts = db.Abouts.ToList();
            model.Skills = db.Skills.Where(s => s.IsActive == true).ToList();
            return View(model);
        }
        public ActionResult Resume()
        {
            var resume = db.Resumes.Where(r => r.IsActive == true).OrderByDescending(r =>r.ResumeId);
            return View(resume.ToList());
        }
        public ActionResult Services()
        {
            var service = db.Services.Where(s => s.IsActive == true).ToList();
            return View(service);
        }
        public ActionResult Portfolio()
        {
            
            var tags= db.Tags.Where(t => t.IsActive == true).ToList();
            return View(tags);
        }
        public ActionResult Blog()
        {
            var blog = db.Blogs.Where(b => b.IsActive == true).OrderByDescending(b => b.BlogId).Take(3).ToList();
            return View(blog);
        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}