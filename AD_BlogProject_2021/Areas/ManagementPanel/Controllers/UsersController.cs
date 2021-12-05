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
    [Auth]
    public class UsersController : Controller
    {
        private MyBlogContext db = new MyBlogContext();

        // GET: ManagementPanel/Users
        public ActionResult Index()
        {
           var users = db.Users.Include(u => u.Roles).Include(u => u.UserDetails);
           return View(users.ToList());
        }

        // GET: ManagementPanel/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: ManagementPanel/Users/Create
        public ActionResult Create()
        {
            ViewBag.RolId = new SelectList(db.Roles, "RolId", "RolName");
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "Website");
            return View();
        }

        // POST: ManagementPanel/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "UserId,UserName,Email,Password,IsActive,RegisterDate,RolId,UserDetails")] Users users)
        {
            if (ModelState.IsValid)
            {
                
                users.RegisterDate = DateTime.Now;
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RolId = new SelectList(db.Roles, "RolId", "RolName", users.RolId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "Website", users.UserId);
            return View(users);
        }

        // GET: ManagementPanel/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.RolId = new SelectList(db.Roles, "RolId", "RolName", users.RolId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "Website", users.UserId);
            return View(users);
        }

        // POST: ManagementPanel/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Email,Password,IsActive,RegisterDate,RolId,UserDetails")] Users users)
        {
            if (ModelState.IsValid)
            {
                Users user = db.Users.Find(users.UserId);
                user.UserName = users.UserName;
                user.Email = users.Email;
                user.Password = users.Password;
                user.IsActive = users.IsActive;
                user.RegisterDate = DateTime.Now;
                user.RolId = users.RolId;
                user.UserDetails.BirthDay = users.UserDetails.BirthDay;
                user.UserDetails.Age = users.UserDetails.Age;
                user.UserDetails.Website = users.UserDetails.Website;
                user.UserDetails.City = users.UserDetails.City;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RolId = new SelectList(db.Roles, "RolId", "RolName", users.RolId);
            ViewBag.UserId = new SelectList(db.UserDetails, "UserId", "Website", users.UserId);
            return View(users);
        }

        // GET: ManagementPanel/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: ManagementPanel/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.Users.Find(id);
            db.UserDetails.Remove(users.UserDetails);
            db.Users.Remove(users);
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
