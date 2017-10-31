using IMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SqlLiteProjectRepository rep = new Repository.SqlLiteProjectRepository();
            rep.GetProject(1);

            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}