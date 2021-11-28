using AD_BlogProject_2021.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AD_BlogProject_2021.Controllers
{
    public class AccountController : Controller
    {
        MyBlogContext db = new MyBlogContext();
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name,string Email,string Password)
        {
            Users user = new Users();
            if (!String.IsNullOrEmpty(Name) && !String.IsNullOrEmpty(Email) && !String.IsNullOrEmpty(Password))
            {
                user.UserName = Name;
                user.Email = Email;
                user.Password = Password;
                user.IsActive = true;
                user.RegisterDate = DateTime.Now;
                foreach (var item in db.Roles)
                {
                    if (item.RolId == 3)
                    {
                        user.RolId = item.RolId;
                    }
                }
                UserDetails detail = new UserDetails();
                detail.BirthDay = null;
                detail.Age = null;
                detail.Website =null;
                detail.City = null;
                user.UserDetails = detail;
                db.UserDetails.Add(detail);
                db.Users.Add(user);
                db.SaveChanges();
            }
            else
            {
                return View("Veriler boş geldi");
            }
            return RedirectToAction("Login","Account");
        }


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email,string Password)
        {
            if (Email != null && Password != null)
            {
                Users user = db.Users.FirstOrDefault(u => u.Password == Password && u.Email == Email);
                if (user != null)
                {
                    if (user.RolId == 1)
                    {
                        Session["Kullanıcı Adı"] = user.UserName;
                        return RedirectToAction("Index", "Blogs", new { area = "ManagementPanel" });
                    }
                    else
                    {
                        Session["KullanıcıAdı"] = user.UserName;
                        return RedirectToAction("Index", "Home");
                    }

                }
               

            }

            return View();
            
 
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}