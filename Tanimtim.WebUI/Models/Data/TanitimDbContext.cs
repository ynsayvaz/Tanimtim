using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tanimtim.WebUI.Models.DTO;
using Tanimtim.WebUI.Models.Entities;

namespace Tanimtim.WebUI.Models.Data
{
    public class TanitimDbContext:DbContext
    {
        public TanitimDbContext():base("name = Tanitim")
        {

        }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Hizmetlerimiz> Hizmetlerimiz { get; set;}
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Galeriler> Galeriler { get; set; }
        public DbSet<Sliderlar> Sliderlar { get; set; }
        public DbSet<Kategoriler> Kategoriler { get; set; }
        public DbSet<Bloglar> Bloglar { get; set; }
        public DbSet<BlogYorumlari> BlogYorumlari { get; set; }
        public DbSet<BlogYorumlariDTO> BlogYorumlariDTO { get; set; }
       
    }
}