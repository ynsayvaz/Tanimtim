using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tanimtim.WebUI.Models.Entities
{
    [Table("BlogYorumlari")]
    public class BlogYorumlari
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string YorumAciklama { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public DateTime YorumTarihi { get; set; }
        public bool  AktifMi { get; set; }
    }
}