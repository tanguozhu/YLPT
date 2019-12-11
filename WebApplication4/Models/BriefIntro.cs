using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication4.Models
{
    public class BriefIntro 
    {
       public int Id { get; set; }
       public string Introduction { get; set; }
       public string Pic { get; set; }
    }
}
