using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AD_BlogProject_2021.Models.EntityFramework;
using FiveSessionWebMvcApp.Areas.ManagementPanel.Helpers;

namespace AD_BlogProject_2021.Areas.ManagementPanel.Controllers
{
    public class BlogsController : Controller
    {
        private MyBlogContext db = new MyBlogContext();

        // GET: ManagementPanel/Blogs
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.BlogDetails);
            return View(blogs.ToList());
        }

        // GET: ManagementPanel/Blogs/Details/5
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
            return View(blogs);
        }

        // GET: ManagementPanel/Blogs/Create
        public ActionResult Create()
        {
            ViewBag.BlogId = new SelectList(db.BlogDetails, "BlogId", "Description");
            ViewBag.TagId = db.Tags.ToList();
            return View();
        }

        // POST: ManagementPanel/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "BlogId,Title,SubTitle,ImageURL,IsActive,RegisterDate,BlogDetails")] Blogs blogs, List<int> TagId, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                if (TagId.Count() > 0)
                {
                    foreach (var item in TagId)
                    {
                        blogs.Tags.Add(db.Tags.Find(item));
                    }
                }
                if (imgFile != null)
                {
                    blogs.ImageURL = ImageUpload.SaveImage(imgFile, 569, 285);
                }
                blogs.RegisterDate = DateTime.Now;
                db.Blogs.Add(blogs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlogId = new SelectList(db.BlogDetails, "BlogId", "Description", blogs.BlogId);
            return View(blogs);
        }

        // GET: ManagementPanel/Blogs/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.BlogId = new SelectList(db.BlogDetails, "BlogId", "Description", blogs.BlogId);
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "TagName");
            return View(blogs);
        }

        // POST: ManagementPanel/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "BlogId,Title,SubTitle,ImageURL,IsActive,RegisterDate,BlogDetails")] Blogs blogs,HttpPostedFileBase imgFile,List<int>TagId)
        {
            if (ModelState.IsValid)
            {
                Blogs update = db.Blogs.Find(blogs.BlogId);
                if (imgFile!= null)
                {
                    ImageUpload.DeleteByPath(update.ImageURL);
                    update.ImageURL = ImageUpload.SaveImage(imgFile, 569, 285);
                }
                update.Title = blogs.Title;
                update.SubTitle = blogs.SubTitle;
                update.IsActive = blogs.IsActive;
                update.RegisterDate = DateTime.Now;
                if (TagId.Count() > 0)
                {
                    update.Tags.Clear();
                    foreach (var item in TagId)
                    {
                        update.Tags.Add(db.Tags.Find(item));
                    }
                }
                update.BlogDetails.Description = blogs.BlogDetails.Description;

                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlogId = new SelectList(db.BlogDetails, "BlogId", "Description", blogs.BlogId);
            return View(blogs);
        }

        // GET: ManagementPanel/Blogs/Delete/5
        public ActionResult Delete(int? id)
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
            return View(blogs);
        }

        // POST: ManagementPanel/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blogs blogs = db.Blogs.Find(id);
            BlogDetails details = db.BlogDetails.Find(id);
            
            blogs.Tags.Clear();
            foreach (var item in blogs.Comments.ToList())
            {
                db.Comments.Remove(item);
            }
            ImageUpload.DeleteByPath(blogs.ImageURL);
            db.BlogDetails.Remove(details);
            db.Blogs.Remove(blogs);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
