using IMS.Models;
using IMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IMS.Controllers
{
    public class AdminController : Controller
    {
        AdminRepository rep = new AdminRepository();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            Admin admin = rep.GetAdmin(username, password);
            if (admin != null)
            {
                MyFormsAuthentication.SetAuthCookie(admin.UserName, admin, false);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Logout()
        {
            var cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Response.Cookies.Remove(cookie.Name);
            HttpContext.Response.Cookies.Add(cookie);
            return Redirect("/admin/login");
        }
    }
}