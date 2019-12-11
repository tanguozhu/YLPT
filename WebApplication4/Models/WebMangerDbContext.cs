using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class WebMangerDbContext : DbContext
    {
        public DbSet<WebManger> WebMangers { get; set; }
    }
}
