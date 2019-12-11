using System;
using System.Web;
using System.Data.Entity;
namespace WebApplication4.Models
{
    public class WorkflowDbcontext : DbContext
    {
        public DbSet<Workflow> Workflows { get; set; }
    }
}
