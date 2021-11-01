using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaglikOcagi.Entity;
using SaglikOcagi.Repository;

namespace SaglikOcagi.Areas.Admin.Controllers
{
    public class MesaiCizelgeController : Controller
    {
        // GET: Admin/MesaiCizelge
        BaseRepository<tbl_MesaiCizelge> cizelge = new BaseRepository<tbl_MesaiCizelge>();
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View();
        }

        public ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(cizelge.SelectAll());
        }

        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            tbl_MesaiCizelge czg = cizelge.GetByCizelgeID(id);
            cizelge.Delete(czg);
            cizelge.Save();

            return RedirectToAction("List");
        }

        public ActionResult AddCizelge()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddCizelge(tbl_MesaiCizelge model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.CizelgeFoto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.CizelgeFoto = 0.ToString();
            }

            if (ModelState.IsValid)
            {
                cizelge.Insert(model);
                cizelge.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }


        public ActionResult EditCizelge(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(cizelge.GetByCizelgeID(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditCizelge(tbl_MesaiCizelge model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.CizelgeFoto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.CizelgeFoto = 0.ToString();
            }

            if (ModelState.IsValid)
            {
                cizelge.Update(model);
                cizelge.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }
    }
}