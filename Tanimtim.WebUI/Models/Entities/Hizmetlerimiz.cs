using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tanimtim.WebUI.Models.Entities
{
    [Table("Hizmetlerimiz")]
    public class Hizmetlerimiz
    {
        [Key]
        [DisplayName("Kimlik")]
        public int Id { get; set; }
        [DisplayName("Başlık")]
        public string Baslik { get; set; }
        [DisplayName("İçerik")]
        public string Icerik { get; set; }
        [DisplayName("Resim")]
        public string ResimUrl { get; set; }
    }
}