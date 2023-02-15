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
    public class BlogYorumlariController : Controller
    {
        private TanitimDbContext db = new TanitimDbContext();

        private Bloglar blog = new Bloglar();
        // GET: BlogYorumlari
       // [Route("~/BlogYorumlari")]
        public ActionResult Index()
        {
            var blogYorum = (from b in db.BlogYorumlari.ToList()
                    join bl in db.Bloglar.ToList() on b.BlogId equals bl.Id
                    select new BlogYorumlariDTO()
                    {
                        Id = b.Id,
                        YorumAciklama = b.YorumAciklama,
                        AdSoyad = b.AdSoyad,
                        Email = b.Email,
                        YorumTarihi = b.YorumTarihi,
                        AktifMi = b.AktifMi == true ? "Aktif" : "Pasif" 
                    }
                ).ToList();
            return View(blogYorum);
        }

        public ActionResult Ekle()
        {
            ViewBag.BlogId = new SelectList(db.Bloglar, "Id", "BlogAdi",
                db.Bloglar.Where(x => x.Id == blog.Id));
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(BlogYorumlari blogyorum)
        {
            db.BlogYorumlari.Add(blogyorum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Guncelle(int? Id)
        {
            if (Id == null)
                return RedirectToAction("Index");
            BlogYorumlari b = db.BlogYorumlari.Where(x => x.Id == (int)Id).SingleOrDefault();
            ViewBag.BlogId = new SelectList(db.Bloglar, "Id", "BlogAdi", b.BlogId);
            return View(b);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Guncelle(BlogYorumlari blogyorum)
        {
            if (ModelState.IsValid)
            {

                BlogYorumlari b = db.BlogYorumlari.Where(x => x.Id == blogyorum.Id).SingleOrDefault();
                if (b != null)
                {
                    b.AdSoyad = blogyorum.AdSoyad;
                    b.BlogId = blogyorum.BlogId;
                    b.Email = blogyorum.Email;
                    b.YorumAciklama = blogyorum.YorumAciklama;
                    b.YorumTarihi = blogyorum.YorumTarihi;
                    b.AktifMi = blogyorum.AktifMi;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(blogyorum);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            BlogYorumlari blogyorum = db.BlogYorumlari.Where(x => x.Id == id).SingleOrDefault();
            if (blog != null)
            {
                db.BlogYorumlari.Remove(blogyorum);
                db.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}