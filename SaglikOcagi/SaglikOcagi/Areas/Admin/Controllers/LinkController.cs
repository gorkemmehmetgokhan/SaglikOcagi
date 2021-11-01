using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaglikOcagi.Entity;
using SaglikOcagi.Repository;

namespace SaglikOcagi.Areas.Admin.Controllers
{
    public class LinkController : Controller
    {
        // GET: Admin/Link
        BaseRepository<tbl_Linkler> link = new BaseRepository<tbl_Linkler>();

        public ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(link.SelectAll());
        }

        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            tbl_Linkler lnk = link.GetByLinkID(id);
            link.Delete(lnk);
            link.Save();

            return RedirectToAction("List");
        }

        public ActionResult AddLink()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddLink(tbl_Linkler model)
        {

            if (ModelState.IsValid)
            {
                link.Insert(model);
                link.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }


        public ActionResult EditLink(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(link.GetByLinkID(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditLink(tbl_Linkler model)
        {
            if (ModelState.IsValid)
            {
                link.Update(model);
                link.Save();
                return RedirectToAction("List");
            }
            else
                return View();
        }
    }
}