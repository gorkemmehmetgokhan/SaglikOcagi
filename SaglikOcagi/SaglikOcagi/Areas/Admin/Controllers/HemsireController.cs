using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaglikOcagi.Entity;
using SaglikOcagi.Repository;

namespace SaglikOcagi.Areas.Admin.Controllers
{
    public class HemsireController : Controller
    {
        // GET: Admin/Hemsire
        BaseRepository<tbl_Hemsire> hemsire = new BaseRepository<tbl_Hemsire>();

        public ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(hemsire.SelectAll());
        }

        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            tbl_Hemsire hkm = hemsire.GetByHemsireID(id);
            hemsire.Delete(hkm);
            hemsire.Save();

            return RedirectToAction("List");
        }

        public ActionResult AddHemsire()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddHemsire(tbl_Hemsire model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.HemsireFoto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.HemsireFoto = 0.ToString();
            }

            if (ModelState.IsValid)
            {

                hemsire.Insert(model);
                hemsire.Save();
                return RedirectToAction("List");

            }
            else
                return View();
        }


        public ActionResult EditHemsire(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(hemsire.GetByHemsireID(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditHemsire(tbl_Hemsire model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.HemsireFoto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.HemsireFoto = 0.ToString();
            }

            if (ModelState.IsValid)
            {

                hemsire.Update(model);
                hemsire.Save();
                return RedirectToAction("List");

            }
            else
                return View();
        }
    }
}