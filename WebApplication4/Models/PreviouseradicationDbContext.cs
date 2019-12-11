using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class PreviouseradicationDbContext : DbContext
    {
        public DbSet<Previouseradication> Previouseradications { get; set; }

        public System.Data.Entity.DbSet<WebApplication4.Models.Visit> Visits { get; set; }
    }
}
