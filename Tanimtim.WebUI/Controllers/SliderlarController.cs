using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Tanimtim.WebUI.Models.Data;
using Tanimtim.WebUI.Models.Entities;

namespace Tanimtim.WebUI.Controllers
{
    public class SliderlarController : Controller
    {
        TanitimDbContext _db = new TanitimDbContext();
        // GET: Sliderlar
        public ActionResult Index()
        {
            List<Sliderlar> SliderlarListeHali = _db.Sliderlar.ToList();
            return View(SliderlarListeHali);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(Sliderlar sliderlar, HttpPostedFileBase Resim)
        {

            if (Resim != null)
            {
                WebImage img = new WebImage(Resim.InputStream);
                FileInfo imgInfo = new FileInfo(Resim.FileName);
                string imgName = imgInfo.Name;
                img.Resize(640, 480);
                img.Save("~/Uploads/Sliderlar/" + imgName);
                sliderlar.ResimUrl = "/Uploads/Sliderlar/" + imgName;
            }
            _db.Sliderlar.Add(sliderlar);
            int count=_db.SaveChanges();

            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            Sliderlar Sliderlar = _db.Sliderlar.Where(x => x.Id == id).SingleOrDefault();
            return View(Sliderlar);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Guncelle(Sliderlar sliderlar, HttpPostedFileBase resim, string EskiResim)
        {
            if (ModelState.IsValid)
            {
                Sliderlar s = _db.Sliderlar.Where(x => x.Id == sliderlar.Id).SingleOrDefault();
                if (s != null)
                {
                    if (resim != null)
                    {
                        //if (System.IO.File.Exists(Server.MapPath(EskiResim)))
                        //{
                        //System.IO.File.Delete(Server.MapPath(EskiResim));
                        // }

                        WebImage img = new WebImage(resim.InputStream);
                        FileInfo imgInfo = new FileInfo(resim.FileName);
                        string resimName = imgInfo.Name;
                        img.Resize(640, 480);
                        img.Save("~/Uploads/Sliderlar/" + resimName);
                        sliderlar.ResimUrl = "/Uploads/Sliderlar/" + resimName;
                    }

                    s.Baslik = sliderlar.Baslik;
                    s.Aciklama = sliderlar.Aciklama;
                    s.ResimUrl = sliderlar.ResimUrl;
                    s.EklenemTarihi = sliderlar.EklenemTarihi;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(sliderlar);
            }
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            Sliderlar Sliderlar = _db.Sliderlar.Where(x => x.Id == Id).SingleOrDefault();
            if (Sliderlar != null)
            {
                ////if (System.IO.File.Exists(Server.MapPath(Sliderlar.ResimUrl)))
                //{
                //    System.IO.File.Delete(Server.MapPath(Sliderlar.ResimUrl));
                //}

                _db.Sliderlar.Remove(Sliderlar);
                _db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);

        }

    }
}