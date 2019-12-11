using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class AdversereactionsDbContext : DbContext
    {
        public DbSet<Adversereactions> Adversereactions { get; set; }
    }
}
