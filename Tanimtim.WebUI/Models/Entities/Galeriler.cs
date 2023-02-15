using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tanimtim.WebUI.Models.Entities
{
    [Table("Galeriler")]
    public class Galeriler
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Resim Url")]
        public string ResimUrl { get; set; }
        [DisplayName("Resim Açıklama")]
        public string ResimAciklama { get; set; }
    }
}