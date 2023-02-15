using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tanimtim.WebUI.Models.DTO
{
    
    public class BlogYorumlariDTO
    {
        public int Id { get; set; }
        
        public string YorumAciklama { get; set; }
        
        public string AdSoyad { get; set; }
       
        public string Email { get; set; }
        public DateTime YorumTarihi { get; set; }
        public string AktifMi { get; set; }
    }
}