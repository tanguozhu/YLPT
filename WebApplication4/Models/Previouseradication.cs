using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication4.Models
{
    public class Previouseradication 
    {
        public int RefId;
        public string Phone;
        public int Id { get; set; }
        public string Radicationtime { get; set; }
        public DateTime Radicationdate { get; set; }
        public string Radicationcase { get; set; }
        public string Course { get; set; }
        public string Result { get; set; }
    }
}
