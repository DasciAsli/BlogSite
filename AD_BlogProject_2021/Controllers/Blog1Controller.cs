using AD_BlogProject_2021.Models.EntityFramework;
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
            var blog = db.Blogs.Where(b => b.IsActive == true).OrderByDescending(b => b.BlogId).ToList();
            ViewBag.TagId = db.Tags.Where(t => t.IsActive== true).ToList();
            return View(blog);
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