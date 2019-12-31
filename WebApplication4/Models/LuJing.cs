using System;
using System.Web;

namespace WebApplication4.Models
{
    public class LuJing 
    {
        public int Id { get; set; }
        public string scheme { get; set; }
        public string rs { get; set; }
        public string rs_num { get; set; }
        public int hospital_time { get; set; }
        public int cost_all { get; set; }
        public int cost_town { get; set; }
        public int cost_country { get; set; }
    }
}
