using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApplication4.Models
{
    public class ReturnVisit 
    {
        public int Id { get; set; }
        [Display(Name = "筛查号")]
        public string scannum { get; set; }
        [Display(Name = "身份证号")]
        public string Identinum { get; set; }


        [Display(Name = "就诊")]
        public string VisitId { get; set; }
        [Display(Name = "就诊日期")]
        public string Date { get; set; }
        [Display(Name = "姓名")]
        public string Name { get; set; }

        public int returnvisit { get; set; }
        public string returnvisitcontent { get; set; }
        public string returnvisitdate { get; set; }
    }
}
