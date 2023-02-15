using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tanimtim.WebUI.Models.Configuration
{
    public class TanitimDbContext:DbContext
    {

        public TanitimDbContext() : base("EmpDBContext") { }

        public DbSet<Employee> Employees { get; set; }

    }

}