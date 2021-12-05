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
    [Auth]
    public class HomesController : Controller
    {
        private MyBlogContext db = new MyBlogContext();

        // GET: ManagementPanel/Homes
        public ActionResult Index()
        {
            return View(db.Homes.ToList());
        }

        // GET: ManagementPanel/Homes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homes homes = db.Homes.Find(id);
            if (homes == null)
            {
                return HttpNotFound();
            }
            return View(homes);
        }

        // GET: ManagementPanel/Homes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagementPanel/Homes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HomeId,ImageURL,Title,Subtitle,IsActive,RegisterDate")] Homes homes,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                if (imgFile !=null)
                {
                    homes.ImageURL = ImageUpload.SaveImage(imgFile, 625, 665);
                }
                homes.RegisterDate = DateTime.Now;
                db.Homes.Add(homes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homes);
        }

        // GET: ManagementPanel/Homes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homes homes = db.Homes.Find(id);
            if (homes == null)
            {
                return HttpNotFound();
            }
            return View(homes);
        }

        // POST: ManagementPanel/Homes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HomeId,ImageURL,Title,Subtitle,IsActive,RegisterDate")] Homes homes,HttpPostedFileBase imgFile)//1.Resim yüklemek için birinci aşama parametre olarak resmi posta gönder
        {
            if (ModelState.IsValid)
            {
                Homes upload = db.Homes.Find(homes.HomeId);// 2)Resmi içeren homes nesnesini bul ve uploada ata
                if (imgFile != null)
                {
                    ImageUpload.DeleteByPath(upload.ImageURL);//3)Önceki resmi sil.Projede yer kaplamaması çin bunu yapmalısın
                    upload.ImageURL = ImageUpload.SaveImage(imgFile, 625, 665);//4)Yeni resmi upload nesnesine ata
                }              
                upload.Title = homes.Title;
                upload.Subtitle = homes.Subtitle;
                upload.IsActive = homes.IsActive;
                upload.RegisterDate = DateTime.Now;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(homes);
        }

        // GET: ManagementPanel/Homes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homes homes = db.Homes.Find(id);
            if (homes == null)
            {
                return HttpNotFound();
            }
            return View(homes);
        }

        // POST: ManagementPanel/Homes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Homes homes = db.Homes.Find(id);
            ImageUpload.DeleteByPath(homes.ImageURL);//Silme işlemindede resmi silmeyi unutma projeden
            db.Homes.Remove(homes);
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
