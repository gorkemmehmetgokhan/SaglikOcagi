using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaglikOcagi.Entity;
using SaglikOcagi.Repository;
using System.Data.Entity;

namespace SaglikOcagi.Areas.Admin.Controllers
{
    public class AdresController : Controller
    {
        // GET: Admin/Adres
        BaseRepository<tbl_Adres> adres = new BaseRepository<tbl_Adres>();
        DB_SaglikMerkeziEntities ctx = new DB_SaglikMerkeziEntities();
       
        public ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(adres.SelectAll());
        }

        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            tbl_Adres adr = adres.GetByAddressID(id);
            adres.Delete(adr);
            adres.Save();

            return RedirectToAction("List");
        }

        public ActionResult AddAddress()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddAddress(tbl_Adres model)
        {
           
            if (ModelState.IsValid)
            {
                adres.Insert(model);
                adres.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }


        public ActionResult EditAddress(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }           
            return View(adres.GetByAddressID(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditAddress(tbl_Adres model)
        {            
            if (ModelState.IsValid)
            {              
                adres.Update(model);
                adres.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }
    }
}