using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class ReturnVisitDbContext : DbContext
    {
        public DbSet<ReturnVisit> returnVisits { get; set; }
    }
}
