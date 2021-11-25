using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AD_BlogProject_2021.Models.EntityFramework;

namespace AD_BlogProject_2021.Areas.ManagementPanel.Controllers
{
    public class TagsController : Controller
    {
        private MyBlogContext db = new MyBlogContext();

        // GET: ManagementPanel/Tags
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }

        // GET: ManagementPanel/Tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tags tags = db.Tags.Find(id);
            if (tags == null)
            {
                return HttpNotFound();
            }
            return View(tags);
        }

        // GET: ManagementPanel/Tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagementPanel/Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TagId,TagName,IsActive,RegisterDate")] Tags tags)
        {
            if (ModelState.IsValid)
            {
                tags.RegisterDate = DateTime.Now;
                db.Tags.Add(tags);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tags);
        }

        // GET: ManagementPanel/Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tags tags = db.Tags.Find(id);
            if (tags == null)
            {
                return HttpNotFound();
            }
            return View(tags);
        }

        // POST: ManagementPanel/Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TagId,TagName,IsActive,RegisterDate")] Tags tags)
        {
            if (ModelState.IsValid)
            {
                tags.RegisterDate = DateTime.Now;
                db.Entry(tags).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tags);
        }

        // GET: ManagementPanel/Tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tags tags = db.Tags.Find(id);
            if (tags == null)
            {
                return HttpNotFound();
            }
            return View(tags);
        }

        // POST: ManagementPanel/Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tags tags = db.Tags.Find(id);
            foreach (var item in db.Blogs.ToList())
            {
                item.Tags.Remove(tags);
            }
            foreach (var item in db.Portfolios.ToList())
            {
                item.Tags.Remove(tags);
            }
                     
            db.Tags.Remove(tags);
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
