using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication4.Models
{
    public class patient 
    {
        public int id { get; set; }
        public int stage { get; set; }
        
        public int age { get; set; }
        public string insurance { get; set; }

        public int height { get; set; }
        public string area { get; set; }
        public int weight { get; set; }

        public int bestcase { get; set; }
        public int feepredicate { get; set; }
    }
}
