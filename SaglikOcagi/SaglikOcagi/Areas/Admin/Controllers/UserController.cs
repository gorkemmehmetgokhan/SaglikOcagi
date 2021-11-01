using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaglikOcagi.Entity;
using SaglikOcagi.Repository;

namespace SaglikOcagi.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User

        BaseRepository<tbl_Kullanici> user = new BaseRepository<tbl_Kullanici>();

        public ActionResult List()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(user.SelectAll());
        }

        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            tbl_Kullanici usr = user.GetByUserID(id);
            user.Delete(usr);
            user.Save();

            return RedirectToAction("List");
        }

        public ActionResult AddUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddUser(tbl_Kullanici model)
        {

            if (ModelState.IsValid)
            {
                user.Insert(model);
                user.Save();
                return RedirectToAction("List");

            }
            else
                return View();
        }


        public ActionResult EditUser(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "../SignUp");
            }
            return View(user.GetByUserID(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditUser(tbl_Kullanici model)
        {          
            if (ModelState.IsValid)
            {
                user.Update(model);
                user.Save();
                return RedirectToAction("List");

            }
            else
                return View();
        }
    }
}