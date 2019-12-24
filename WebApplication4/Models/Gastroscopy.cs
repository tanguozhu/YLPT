using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace WebApplication4.Models
{
    public class Gastroscopy 
    {
        public int Id { get; set; }
        public int PastId;
        public string Visit { get; set; }
        public string scannum { get; set; }
        public string Checkname { get; set; }
        public string Other { get; set; }
       
        [Display(Name = "检查时间")]
        public string Checktime { get; set; }
        public DateTime dt;
        [Display(Name = "检查号")]
        public string Checknum { get; set; }
        [Display(Name = "结论")]
        public string Conclusion { get; set; }
        [Display(Name = "病理号")]
        public string Pathologynum { get; set; }
        [Display(Name = "病理结论")]
        public string Pathologyconclusion { get; set; }
        
        public string Pic1 { get; set; }
        public string Pic2 { get; set; }
        public string Pic3 { get; set; }
        public string Pic4 { get; set; }
        public string Pic5 { get; set; }
        public string Pic6 { get; set; }
        public string Pic7 { get; set; }
        public string Pic8 { get; set; }

        public int isfollowup { get; set; }
        public DateTime followuptime { get; set; }
        public DataTable getweinianmo;
        



    }
}
