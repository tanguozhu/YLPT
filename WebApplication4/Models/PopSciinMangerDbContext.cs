using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class PopSciinMangerDbContext : DbContext
    {
        public DbSet<PopSciinManger> PopSciinMangers { get; set; }
    }
}
