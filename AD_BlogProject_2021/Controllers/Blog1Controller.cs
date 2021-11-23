using AD_BlogProject_2021.Models.EntityFramework;
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
            model.Blogs = db.Blogs.Where(u => u.IsActive == true).ToList();            
            model.Tags = db.Tags.Where(t => t.IsActive== true).ToList();
            return View(model);
        }
        public ActionResult SearchFilter(string arama)
        {
            var model = db.Blogs.Where(u => u.IsActive == true).ToList();
            if (!string.IsNullOrEmpty(arama))
            {
                model = db.Blogs.Where(u => u.IsActive == true && ( u.SubTitle.Contains(arama) || u.BlogDetails.Description.Contains(arama))).ToList();
            }
            else
            {
                model = db.Blogs.Where(u => u.IsActive == true).ToList();
            }

            return PartialView("_BlogFilter", model);
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
        public ActionResult Details(int id)
        {
            var model = new BlogViewModel();
            model.Blogs = db.Blogs.Where(b => b.BlogId == id).ToList();
            model.Tags = db.Tags.Where(t => t.IsActive == true).ToList();
            return View(model);
  
        }
        public ActionResult BlogComments(int BlogId, string name, string comment)
        {
            Comments comments = new Comments();
            comments.BlogId = BlogId;
            comments.CommentatorName = name;           
            comments.Comment = comment;
            comments.RegisterDate = DateTime.Now;
            db.Comments.Add(comments);
            db.SaveChanges();

            var model = db.Comments.Where(u => u.BlogId == BlogId).ToList();

            return PartialView("_CommentList", model);
        }
        public ActionResult About()
        {

            return View();
        }
    }
}