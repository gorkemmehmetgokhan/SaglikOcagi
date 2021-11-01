using SaglikOcagi.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SaglikOcagi.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            //kullanıcının oturum acma islemi basarili ise
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = User.Identity.Name;
                return RedirectToAction("Index", "SignUp");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(tbl_Kullanici k)
        {
            //validation control
            if (ModelState.IsValid)
            {
                using (DB_SaglikMerkeziEntities ctx = new DB_SaglikMerkeziEntities())
                {
                    var user = ctx.tbl_Kullanici.FirstOrDefault(x => x.KullaniciAdi == k.KullaniciAdi && x.Sifre == k.Sifre);
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(user.KullaniciAdi, true);
                        FormsAuthentication.SetAuthCookie(user.KullaniciID.ToString(), true);
                        return RedirectToAction("Index", "SignUp");
                    }
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "SignUp");
        }
    }
}