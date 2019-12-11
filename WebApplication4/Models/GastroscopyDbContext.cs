using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class GastroscopyDbContext : DbContext
    {
        public DbSet<Gastroscopy> Gastroscopies { get; set; }
    }
}
