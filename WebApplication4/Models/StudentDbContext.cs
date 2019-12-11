using System;
using System.Data.Entity;
using System.Web;

namespace WebApplication4.Models
{
    public class StudentDbContext :DbContext
    {
        public DbSet<Student> Students { get; set; }
    }
}
