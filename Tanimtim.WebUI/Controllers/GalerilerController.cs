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
    [Route("~/Galeriler/")]
    public class GalerilerController : Controller
    {
        TanitimDbContext _db = new TanitimDbContext();
        // GET: Galeriler
        [Route("Index")]
        public ActionResult Index()
        {
            List<Galeriler> galerilers = _db.Galeriler.ToList();
            return View(galerilers);
        }
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(Galeriler galeriler, HttpPostedFileBase resim)
        {

            if (resim != null)
            {
                WebImage img = new WebImage(resim.InputStream);
                FileInfo imgInfo = new FileInfo(resim.FileName);
                string imgName = imgInfo.Name;
                img.Resize(640, 480);
                img.Save("~/Uploads/Galeri/" + imgName);
                galeriler.ResimUrl = "/Uploads/Galeri/" + imgName;
            }
            _db.Galeriler.Add(galeriler);
            int count = _db.SaveChanges();

            return Redirect("Index");
        }
        [HttpGet]
        public ActionResult Guncelle(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            Galeriler galeriler = _db.Galeriler.Where(x => x.Id == id).SingleOrDefault();
            return View(galeriler);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Guncelle(Galeriler galeriler, HttpPostedFileBase resim, string EskiResim)
        {
            if (ModelState.IsValid)
            {
                Galeriler g = _db.Galeriler.Where(x => x.Id ==galeriler.Id).SingleOrDefault();
                if (g != null)
                {
                    if (resim != null)
                    {
                        //if (System.IO.File.Exists(Server.MapPath(EskiResim)))
                        //{
                        //    System.IO.File.Delete(Server.MapPath(EskiResim));
                        //}

                        WebImage img = new WebImage(resim.InputStream);
                        FileInfo imgInfo = new FileInfo(resim.FileName);
                        string resimName = imgInfo.Name;
                        img.Resize(640, 480);
                        img.Save("~/Uploads/Galeri/" + resimName);
                        galeriler.ResimUrl = "/Uploads/Galeri/" + resimName;
                    }

                    g.ResimAciklama = galeriler.ResimAciklama;
                    g.ResimUrl = galeriler.ResimUrl;
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
                return View(galeriler);
            }
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            Galeriler galeriler = _db.Galeriler.Where(x => x.Id == Id).SingleOrDefault();
            if (galeriler != null)
            {
                ////if (System.IO.File.Exists(Server.MapPath(galeriler.ResimUrl)))
                //{
                //    System.IO.File.Delete(Server.MapPath(galeriler.ResimUrl));
                //}

                _db.Galeriler.Remove(galeriler);
                _db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}