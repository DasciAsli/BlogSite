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
    public class ResumesController : Controller
    {
        private MyBlogContext db = new MyBlogContext();

        // GET: ManagementPanel/Resumes
        public ActionResult Index()
        {
            return View(db.Resumes.ToList());
        }

        // GET: ManagementPanel/Resumes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resumes resumes = db.Resumes.Find(id);
            if (resumes == null)
            {
                return HttpNotFound();
            }
            return View(resumes);
        }

        // GET: ManagementPanel/Resumes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagementPanel/Resumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResumeId,ResumeTitle,ResumeSubTitle,ResumePlace,ResumeDate,RegisterDate,IsActive")] Resumes resumes)
        {
            if (ModelState.IsValid)
            {
                resumes.RegisterDate = DateTime.Now;
                db.Resumes.Add(resumes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resumes);
        }

        // GET: ManagementPanel/Resumes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resumes resumes = db.Resumes.Find(id);
            if (resumes == null)
            {
                return HttpNotFound();
            }
            return View(resumes);
        }

        // POST: ManagementPanel/Resumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResumeId,ResumeTitle,ResumeSubTitle,ResumePlace,ResumeDate,RegisterDate,IsActive")] Resumes resumes)
        {
            if (ModelState.IsValid)
            {
                Resumes update = db.Resumes.Find(resumes.ResumeId);
                update.ResumeTitle = resumes.ResumeTitle;
                update.ResumeSubTitle = resumes.ResumeSubTitle;
                update.ResumePlace = resumes.ResumePlace;
                update.ResumeDate = resumes.ResumeDate;
                update.RegisterDate = DateTime.Now;               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resumes);
        }

        // GET: ManagementPanel/Resumes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resumes resumes = db.Resumes.Find(id);
            if (resumes == null)
            {
                return HttpNotFound();
            }
            return View(resumes);
        }

        // POST: ManagementPanel/Resumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Resumes resumes = db.Resumes.Find(id);
            db.Resumes.Remove(resumes);
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
