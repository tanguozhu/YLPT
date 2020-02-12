using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class qiyeDbContext : DbContext
    {
        public DbSet<qiye> Qiyes { get; set; }
    }
}
