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
    public class Hizmetler
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Başlık")]
        public string Baslik { get; set; }
        public string Icerik { get; set; }
        public string ResimUrl { get; set; }

    }
}