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
    public class LoginController : Controller
    {
        private TanitimDbContext _db = new TanitimDbContext();
        // GET: Login
        public ActionResult Index()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult LoginControl(KullaniciDTO kullaniciDTO)
        {
            Kullanici kullanici = _db.Kullanicilar.Where(x => x.AdSoyad == kullaniciDTO.KullaniciAdi && x.Sifre == kullaniciDTO.Sifre && x.AktifMi == true).SingleOrDefault();
            if (kullanici == null)
                return Json("", JsonRequestBehavior.AllowGet);
            else
            {
                Session["UserManager"] = kullanici;
                return Json("/Admin/Index", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
