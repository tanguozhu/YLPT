﻿using System;
using System.Web;
using System.Data.Entity;

namespace WebApplication4.Models
{
    public class DocMangerDbContext : DbContext
    {
        public DbSet<DocManger> DocMangers { get; set; }
    }
}
