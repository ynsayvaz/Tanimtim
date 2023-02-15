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
    public class HakkimizdaController : Controller
    {
        private TanitimDbContext _db = new TanitimDbContext();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            List<Hakkimizda> hakkimizdaListeHali = _db.Hakkimizda.ToList();
            return View(hakkimizdaListeHali);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(Hakkimizda hakkimizda, HttpPostedFileBase resim) 
        {
            
            if(resim != null)
            {
                WebImage img = new WebImage(resim.InputStream);
                FileInfo imgInfo = new FileInfo(resim.FileName);
                string imgName = imgInfo.Name;
                img.Resize(640, 480);
                img.Save("~/Uploads/Hakkimizda/" + imgName);
                hakkimizda.ResimUrl = "/Uploads/Hakkimizda/" + imgName;
            }
            _db.Hakkimizda.Add(hakkimizda);
            int count = _db.SaveChanges();

            return Redirect("Index");
        }

        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            Hakkimizda hakkimizda = _db.Hakkimizda.Where(x => x.Id == id).SingleOrDefault();
            return View(hakkimizda);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Guncelle(Hakkimizda hakkimizda, HttpPostedFileBase resim, string EskiResim)
        {
            if (ModelState.IsValid)
            {
                Hakkimizda h = _db.Hakkimizda.Where(x => x.Id == hakkimizda.Id).SingleOrDefault();
                if (h != null)
                {
                    if (resim != null)
                    {
                        if (System.IO.File.Exists(Server.MapPath(EskiResim)))
                        {
                            System.IO.File.Delete(Server.MapPath(EskiResim));
                        }

                        WebImage img = new WebImage(resim.InputStream);
                        FileInfo imgInfo = new FileInfo(resim.FileName);
                        string resimName = imgInfo.Name;
                        img.Resize(640, 480);
                        img.Save("~/Uploads/Hakkimizda/" + resimName);
                        hakkimizda.ResimUrl = "/Uploads/Hakkimizda/" + resimName;
                    }

                    h.Baslik = hakkimizda.Baslik;
                    h.Icerik = hakkimizda.Icerik;
                    h.ResimUrl = hakkimizda.ResimUrl;
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
                return View(hakkimizda);
            }
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            Hakkimizda hakkimizda = _db.Hakkimizda.Where(x => x.Id == Id).SingleOrDefault();
            if (hakkimizda != null)
            {
                ////if (System.IO.File.Exists(Server.MapPath(hakkimizda.ResimUrl)))
                //{
                //    System.IO.File.Delete(Server.MapPath(hakkimizda.ResimUrl));
                //}

                _db.Hakkimizda.Remove(hakkimizda);
                _db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);

        }
    }
}