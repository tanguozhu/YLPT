using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class LuJingDbContext : DbContext
    {
        public DbSet<LuJing> LuJings { get; set; }
    }
}
