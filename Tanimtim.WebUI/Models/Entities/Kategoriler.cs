using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Tanimtim.WebUI.Models.Entities
{
    [Table("Kategoriler")]
    public class Kategoriler
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Kategori İsimleri")]
        public string KategoriAdi { get; set; }

    }
}