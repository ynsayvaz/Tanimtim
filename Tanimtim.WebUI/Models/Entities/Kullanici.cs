using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tanimtim.WebUI.Models.Entities
{
    [Table("Kullanicilar")]
    public class Kullanici
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public string AdSoyad { get; set; }
        public bool AktifMi { get; set; }
        public DateTime CreateDate { get; set; }
        public bool AdminMi { get; set; }
    }
}