using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class VisitDbContext : DbContext
    {
        public DbSet<Visit> Visits { get; set; }

        public System.Data.Entity.DbSet<WebApplication4.Models.Gastroscopy> Gastroscopies { get; set; }
    }
}
