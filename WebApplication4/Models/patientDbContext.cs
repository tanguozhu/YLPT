using System;
using System.Web;
using System.Data.Entity;


namespace WebApplication4.Models
{
    public class patientDbContext : DbContext
    {
        public DbSet<patient> patients { get; set; }
    }
}
