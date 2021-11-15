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
    public class ServicesController : Controller
    {
        private MyBlogContext db = new MyBlogContext();

        // GET: ManagementPanel/Services
        public ActionResult Index()
        {
            return View(db.Services.ToList());
        }

        // GET: ManagementPanel/Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // GET: ManagementPanel/Services/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagementPanel/Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,ServiceName,ServiceDescription,IsActive,RegisterDate")] Services services)
        {
            if (ModelState.IsValid)
            {
                services.RegisterDate = DateTime.Now;
                db.Services.Add(services);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(services);
        }

        // GET: ManagementPanel/Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // POST: ManagementPanel/Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,ServiceName,ServiceDescription,IsActive,RegisterDate")] Services services)
        {
            if (ModelState.IsValid)
            {
                Services update = db.Services.Find(services.ServiceId);
                update.ServiceName = services.ServiceName;
                update.ServiceDescription = services.ServiceDescription;
                update.IsActive = services.IsActive;               
                update.RegisterDate = DateTime.Now;     
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(services);
        }

        // GET: ManagementPanel/Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Services services = db.Services.Find(id);
            if (services == null)
            {
                return HttpNotFound();
            }
            return View(services);
        }

        // POST: ManagementPanel/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Services services = db.Services.Find(id);
            db.Services.Remove(services);
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
