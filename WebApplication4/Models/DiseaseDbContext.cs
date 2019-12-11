using System;
using System.Web;
using System.Data.Entity;
namespace WebApplication4.Models
{
    public class DiseaseDbContext : DbContext
    {
        public DbSet<Disease> Diseases { get; set; }
    }
}
