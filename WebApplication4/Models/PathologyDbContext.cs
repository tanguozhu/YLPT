using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class PathologyDbContext : DbContext
    {
        public DbSet<Pathology> Pathologies { get; set; }
    }
}
