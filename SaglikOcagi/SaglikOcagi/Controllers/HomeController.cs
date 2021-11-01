using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SaglikOcagi.Entity;
using SaglikOcagi.Repository;

namespace SaglikOcagi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        BaseRepository<tbl_Galeri> galeri = new BaseRepository<tbl_Galeri>();
        BaseRepository<tbl_Anasayfa> anasayfa = new BaseRepository<tbl_Anasayfa>();
        BaseRepository<tbl_Hekim> hekim = new BaseRepository<tbl_Hekim>();
        BaseRepository<tbl_Hemsire> hemsire = new BaseRepository<tbl_Hemsire>();
        BaseRepository<tbl_AsiTakvimi> asiTakvimi = new BaseRepository<tbl_AsiTakvimi>();
        BaseRepository<tbl_MesaiCizelge> cizelge = new BaseRepository<tbl_MesaiCizelge>();


        public ActionResult Homepage()
        {
            ViewBag.Anasayfa = "menu-active";
            return View(anasayfa.SelectAll().ToList());
        }

        public ActionResult GalleryList(int page =1)
        {
            ViewBag.Gallery = "menu-active";
            List<tbl_Galeri> pList = galeri.SelectAll().ToList();
            PagedList<tbl_Galeri> model = new PagedList<tbl_Galeri>(pList, page, 9);
            return View(model);
        }

        public ActionResult Hekimler()
        {
            ViewBag.Hekim = "menu-active";
            return View(hekim.SelectAll().ToList());
        }

        public ActionResult HekimDetail(int id)
        {          
            tbl_Hekim hkm = hekim.GetById(id);
            return View(hkm);
        }

        public ActionResult Hemsire()
        {
            ViewBag.Personel = "menu-active";
            return View(hemsire.SelectAll().ToList());
        }

        public ActionResult HemsireDetail(int id)
        {
            tbl_Hemsire hmsr = hemsire.GetById(id);
            return View(hmsr);
        }

        public ActionResult AsiTakvimi()
        {
            ViewBag.AsiTakvimi = "menu-active";
            return View(asiTakvimi.SelectAll().ToList());
        }

        public ActionResult MesaiCizelge()
        {
            ViewBag.MesaiCizelge = "menu-active";
            return View(cizelge.SelectAll().ToList());
        }
      
    }
}