using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class SurveysDbContext : DbContext
    {
        public DbSet<Surveys> Surveys { get; set; }

        public System.Data.Entity.DbSet<WebApplication4.Models.Student> Students { get; set; }
    }
}
