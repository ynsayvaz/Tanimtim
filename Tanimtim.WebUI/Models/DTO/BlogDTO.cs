using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tanimtim.WebUI.Models.DTO
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public string BlogBaslik { get; set; }
        public string BlogAciklama { get; set; }
        public string KategoriAdi { get; set; }
        public string AktifMi { get; set; }
    }
}