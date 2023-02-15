using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tanimtim.WebUI.Models.Entities
{
    [Table("Sliderlar")]
    public class Sliderlar
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Resim Url")]
        public string ResimUrl { get; set; }
        [DisplayName("Başlık")]
        public string Baslik { get; set; }
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Ekleme Tarihi")]
        public DateTime EklenemTarihi { get; set; }

    }
}