using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace WebApplication4.Models
{
    public class FollowUp 
    {
        public int Id { get; set; }
        public int refid;
        public string visit;
        public string scannum;
        public DateTime date { get; set; }
        public DateTime nextdate { get; set; }
        public int followupnum { get; set; }
        public string gaschecknum { get; set; }
        public int treid { get; set; }
        public int gasid { get; set; }
        public DataTable gas;
        public DataTable tre;
        public string content { get; set; }
    }
}
