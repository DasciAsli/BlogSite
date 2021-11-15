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
    public class PortfoliosController : Controller
    {
        private MyBlogContext db = new MyBlogContext();

        // GET: ManagementPanel/Portfolios
        public ActionResult Index()
        {
            return View(db.Portfolios.ToList());
        }

        // GET: ManagementPanel/Portfolios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolios portfolios = db.Portfolios.Find(id);
            if (portfolios == null)
            {
                return HttpNotFound();
            }
            return View(portfolios);
        }

        // GET: ManagementPanel/Portfolios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagementPanel/Portfolios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PortfolioId,ImageUrl,ProjeName,ProjeCategory,IsActive,RegisterDate")] Portfolios portfolios,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                if (imgFile!=null)
                {
                    portfolios.ImageUrl = ImageUpload.SaveImage(imgFile,516,345);
                }
                portfolios.RegisterDate = DateTime.Now;
                db.Portfolios.Add(portfolios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(portfolios);
        }

        // GET: ManagementPanel/Portfolios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolios portfolios = db.Portfolios.Find(id);
            if (portfolios == null)
            {
                return HttpNotFound();
            }
            return View(portfolios);
        }

        // POST: ManagementPanel/Portfolios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PortfolioId,ImageUrl,ProjeName,ProjeCategory,IsActive,RegisterDate")] Portfolios portfolios,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                Portfolios update = db.Portfolios.Find(portfolios.PortfolioId);
                if (imgFile!=null)
                {
                    ImageUpload.DeleteByPath(update.ImageUrl);
                    update.ImageUrl = ImageUpload.SaveImage(imgFile, 516, 345);
                }
                update.ProjeName = portfolios.ProjeName;
                update.ProjeCategory = portfolios.ProjeCategory;
                update.IsActive = portfolios.IsActive;
                update.RegisterDate = DateTime.Now;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portfolios);
        }

        // GET: ManagementPanel/Portfolios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Portfolios portfolios = db.Portfolios.Find(id);
            if (portfolios == null)
            {
                return HttpNotFound();
            }
            return View(portfolios);
        }

        // POST: ManagementPanel/Portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Portfolios portfolios = db.Portfolios.Find(id);
            db.Portfolios.Remove(portfolios);
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
