using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class putongDbContext : DbContext
    {
        public DbSet<putong> Putongs { get; set; }
    }
}
