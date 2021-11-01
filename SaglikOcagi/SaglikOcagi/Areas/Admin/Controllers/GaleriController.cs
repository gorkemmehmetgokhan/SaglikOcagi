using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaglikOcagi.Entity;
using SaglikOcagi.Repository;

namespace SaglikOcagi.Areas.Admin.Controllers
{
    public class GaleriController : Controller
    {
        // GET: Admin/Galeri
        BaseRepository<tbl_Galeri> galeri = new BaseRepository<tbl_Galeri>();
        
        public ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(galeri.SelectAll());
        }

        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            tbl_Galeri gallery = galeri.GetByGalleryID(id);
            galeri.Delete(gallery);
            galeri.Save();

            return RedirectToAction("List");
        }

        public ActionResult AddPhoto()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddPhoto(tbl_Galeri model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.Foto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.Foto = 0.ToString();
            }

            if (ModelState.IsValid)
            {
                galeri.Insert(model);
                galeri.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }

        public ActionResult EditPhoto(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(galeri.GetByGalleryID(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditPhoto(tbl_Galeri model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.Foto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.Foto = 0.ToString();
            }

            if (ModelState.IsValid)
            {
                galeri.Update(model);
                galeri.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }
    }
}