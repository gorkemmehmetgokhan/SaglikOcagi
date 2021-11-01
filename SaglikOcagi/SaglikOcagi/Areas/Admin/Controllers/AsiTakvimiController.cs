using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaglikOcagi.Entity;
using SaglikOcagi.Repository;

namespace SaglikOcagi.Areas.Admin.Controllers
{
    public class AsiTakvimiController : Controller
    {
        // GET: Admin/AsiTakvimi
        BaseRepository<tbl_AsiTakvimi> asiTakvimi = new BaseRepository<tbl_AsiTakvimi>();
      
        public ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(asiTakvimi.SelectAll());
        }

        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            tbl_AsiTakvimi takvim = asiTakvimi.GetByTakvimID(id);
            asiTakvimi.Delete(takvim);
            asiTakvimi.Save();

            return RedirectToAction("List");
        }

        public ActionResult AddTakvim()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddTakvim(tbl_AsiTakvimi model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.TakvimFoto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.TakvimFoto = 0.ToString();
            }

            if (ModelState.IsValid)
            {
                asiTakvimi.Insert(model);
                asiTakvimi.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }


        public ActionResult EditTakvim(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(asiTakvimi.GetByTakvimID(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditTakvim(tbl_AsiTakvimi model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.TakvimFoto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.TakvimFoto = 0.ToString();
            }

            if (ModelState.IsValid)
            {
                asiTakvimi.Update(model);
                asiTakvimi.Save();
                return RedirectToAction("List");      
            }
            else
                return View();
        }
    }
}