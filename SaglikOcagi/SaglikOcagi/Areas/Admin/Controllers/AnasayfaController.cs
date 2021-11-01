using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaglikOcagi.Entity;
using SaglikOcagi.Repository;

namespace SaglikOcagi.Areas.Admin.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Admin/Anasayfa
        BaseRepository<tbl_Anasayfa> anasayfa = new BaseRepository<tbl_Anasayfa>();
        public ActionResult Index()
        {          
            return View();           
        }

        public ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(anasayfa.SelectAll());

        }

        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            tbl_Anasayfa hpage = anasayfa.GetByHomepageID(id);
            anasayfa.Delete(hpage);
            anasayfa.Save();

            return RedirectToAction("List");
        }

        public ActionResult AddHomepage()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddHomepage(tbl_Anasayfa model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.AnasayfaFoto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.AnasayfaFoto = 0.ToString();
            }

            if (ModelState.IsValid)
            {
                anasayfa.Insert(model);
                anasayfa.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }


        public ActionResult EditHomepage(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(anasayfa.GetByHomepageID(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditHomepage(tbl_Anasayfa model, HttpPostedFileBase photoPath)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.AnasayfaFoto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.AnasayfaFoto = 0.ToString();
            }

            if (ModelState.IsValid)
            {
                anasayfa.Update(model);
                anasayfa.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }
    }
}