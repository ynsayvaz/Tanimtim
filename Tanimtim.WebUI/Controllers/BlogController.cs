using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tanimtim.WebUI.Models.Data;
using Tanimtim.WebUI.Models.DTO;
using Tanimtim.WebUI.Models.Entities;

namespace Tanimtim.WebUI.Controllers
{
    [Route("~/Blog/")]
    public class BlogController : Controller
    {
        private TanitimDbContext db = new TanitimDbContext();
        private Kategoriler kat = new Kategoriler();

        // GET: Blog
        public ActionResult Index()
        {
            List<BlogDTO> blog = (from b in db.Bloglar.ToList()
                        join k in db.Kategoriler.ToList() on b.KategoriId equals k.Id
                        select new BlogDTO
                        {
                            Id = b.Id,
                            BlogBaslik = b.BlogBaslik,
                            BlogAciklama = b.BlogAciklama,
                            KategoriAdi = k.KategoriAdi,
                            AktifMi = b.AktifMi == true ? "Aktif" : "Pasif"
                        }
                ).ToList();
            return View(blog);
        }

        [Route("ekle")]
        public ActionResult Ekle()
        {
            //ViewBag.KategoriId = db.Kategoriler.ToList();
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi",
                db.Kategoriler.Where(x => x.Id == kat.Id));
            return View();

        }

        [HttpPost]
        public ActionResult Ekle(Bloglar blog)
        {
            db.Bloglar.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index");
            Bloglar b = db.Bloglar.Where(x => x.Id == (int)Id).SingleOrDefault();
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "Id", "KategoriAdi", b.KategoriId);
            return View(b);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Guncelle(Bloglar blog)
        {
            if (ModelState.IsValid)
            {

                Bloglar b = db.Bloglar.Where(x => x.Id == blog.Id).SingleOrDefault();
                if (b != null)
                {
                    b.BlogBaslik = blog.BlogBaslik;
                    b.BlogAciklama = blog.BlogAciklama;
                    b.KategoriId = blog.KategoriId;
                    b.AktifMi = blog.AktifMi;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(blog);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        [Route("Delete")]
        public JsonResult Delete(int id)
        {
            Bloglar bloglar = db.Bloglar.Where(x => x.Id == id).SingleOrDefault();
            if (bloglar != null)
            {
                ////if (System.IO.File.Exists(Server.MapPath(galeriler.ResimUrl)))
                //{
                //    System.IO.File.Delete(Server.MapPath(galeriler.ResimUrl));
                //}

                db.Bloglar.Remove(bloglar);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}