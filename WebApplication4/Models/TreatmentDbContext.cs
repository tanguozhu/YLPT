using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class TreatmentDbContext : DbContext
    {
        public DbSet<Treatment> Treatments { get; set; }
    }
}
