using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tanimtim.WebUI.Models.Data;
using Tanimtim.WebUI.Models.Entities;

namespace Tanimtim.WebUI.Controllers
{
    public class KategorilerController : Controller
    {
        private TanitimDbContext _db = new TanitimDbContext();
        // GET: Kategoriler
        public ActionResult Index()
        {
            List<Kategoriler> KategorilerListeHali = _db.Kategoriler.ToList();
            return View(KategorilerListeHali);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ekle(Kategoriler kategoriler)
        {
            _db.Kategoriler.Add(kategoriler);
            _db.SaveChanges();
            return Redirect("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            Kategoriler kategoriler = _db.Kategoriler.Where(x => x.Id == id).SingleOrDefault();
            return View(kategoriler);
        }
        [HttpPost]
        public ActionResult Guncelle(Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                Kategoriler k = _db.Kategoriler.Find(kategoriler);
                if (k != null)
                {
                    k.Id = kategoriler.Id;
                    k.KategoriAdi = kategoriler.KategoriAdi;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(kategoriler);
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            Kategoriler kategoriler = _db.Kategoriler.Where(x => x.Id == Id).SingleOrDefault();
            if (kategoriler != null)
            {
                ////if (System.IO.File.Exists(Server.MapPath(Sliderlar.ResimUrl)))
                //{
                //    System.IO.File.Delete(Server.MapPath(Sliderlar.ResimUrl));
                //}

                _db.Kategoriler.Remove(kategoriler);
                _db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);

        }
    }
}