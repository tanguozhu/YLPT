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
        public string content { get; set; }
    }
}
