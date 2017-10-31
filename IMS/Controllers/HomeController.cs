using IMS.Models;
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
        protected Admin admin = null;

      
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            admin = MyFormsAuthentication.GetAuthCookie();
            if (admin == null)
            {
                filterContext.Result = new RedirectResult("/admin/login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
      

        SqlServerProjectRepository rep = new Repository.SqlServerProjectRepository();
        public ActionResult Index()
        {
            return View(admin);
        }

        public JsonResult GetProjectList(int page, int limit)
        {
            List<Project> list = rep.GetProjectList();
            list.ForEach((m) =>
            {
                m.CreateTime = DateTime.Parse(m.CreateTime).ToString("yyyy-MM-dd HH:mm:ss");
            });
            return Json(new { code = 0, msg = "msg", count = list.Count, data = list.Skip((page - 1) * limit).Take(limit) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add(long id)
        {
            Project projet = new Project();
            if (id > 0)
            {
                projet = rep.GetProject(id);
            }
            return View(projet);
        }

        [HttpPost]
        public JsonResult Add(Project project)
        {
            project.CreateBy = admin.UserName;

            long result = rep.SaveProject(project);
            if (result > 0)
            {
                return Json(new { code = 0, mesg = "添加成功" });
            }
            else
            {
                return Json(new { code = 1, mesg = "保存失败" });
            }

        }

        public JsonResult Edit(Project project)
        {
            Project oldProj = rep.GetProject(project.Id);
            UpdateModel<Project>(oldProj);
            oldProj.ModifyBy = admin.UserName;
            if (rep.ModifyProject(oldProj))
            {
                return Json(new { code = 0, mesg = "修改成功" });
            }
            else
            {
                return Json(new { code = 1, mesg = "修改失败" });
            }
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            if (rep.DeleteProject(id))
            {
                return Json(new { code = 0, msg = "删除成功" });
            }
            else
            {
                return Json(new { code = 1, msg = "删除失败" });
            }
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