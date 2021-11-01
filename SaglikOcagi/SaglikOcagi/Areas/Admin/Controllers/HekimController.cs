using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaglikOcagi.Repository;
using SaglikOcagi.Entity;

namespace SaglikOcagi.Areas.Admin.Controllers
{
    public class HekimController : Controller
    {
        // GET: Admin/Hekim

        BaseRepository<tbl_Hekim> hekim = new BaseRepository<tbl_Hekim>();
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
            return View(hekim.SelectAll());
        }

        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            tbl_Hekim hkm = hekim.GetByHekimID(id);
            hekim.Delete(hkm);
            hekim.Save();

            return RedirectToAction("List");
        }

        public ActionResult AddHekim()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddHekim(tbl_Hekim model, HttpPostedFileBase photoPath, string gender)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.HekimFoto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.HekimFoto = 0.ToString();
            }

            if (gender == "Male")
            {
                model.CinsiyetID = 1;
            }
            else
            {
                model.CinsiyetID = 2;
            }

            if (ModelState.IsValid)
            {
                hekim.Insert(model);
                hekim.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }


        public ActionResult EditHekim(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(hekim.GetByHekimID(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditHekim(tbl_Hekim model, HttpPostedFileBase photoPath,string gender)
        {
            string PhotoName = "";

            if (photoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                photoPath.SaveAs(path);
                model.HekimFoto = PhotoName;
            }
            else if (photoPath == null)
            {
                model.HekimFoto = 0.ToString();
            }

            if (gender == "Male")
            {
                model.CinsiyetID = 1;
            }
            else
            {
                model.CinsiyetID = 2;
            }
       
            if (ModelState.IsValid)
            {
                hekim.Update(model);
                hekim.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }
     
    }
}