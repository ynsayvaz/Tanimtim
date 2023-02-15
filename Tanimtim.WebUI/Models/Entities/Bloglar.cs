using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tanimtim.WebUI.Models.Entities
{
    [Table("Bloglar")]
    public class Bloglar
    {
        public int Id { get; set; }
        public string BlogBaslik { get; set; }
        public string BlogAciklama { get; set; }
        public int KategoriId { get; set; }
        public bool AktifMi { get; set; }
    }
}