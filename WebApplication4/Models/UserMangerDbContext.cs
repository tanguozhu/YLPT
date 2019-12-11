using System;
using System.Data.Entity;
using System.Web;

namespace WebApplication4.Models
{
    public class UserMangerDbContext : DbContext
    {
        public DbSet<UserManger> userMangers { get; set; }
    }
}
