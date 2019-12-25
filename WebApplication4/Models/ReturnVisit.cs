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
       
        [Display(Name = "姓名")]
        public string Name { get; set; }

        public DateTime flag_time { get; set; }

        public int weijing_check { get; set; }
        public DateTime weijing_check_time { get; set; }
        public int weinianmo_check { get; set; }
        public DateTime weinianmo_check_time { get; set; }
        public int youmen_check { get; set; }
        public DateTime youmen_check_time { get; set; }
        public int other_check { get; set; }
        public DateTime other_check_time { get; set; }
        public int youmen_treat { get; set; }
        public DateTime youmen_treat_time { get; set; }
        public int operater_treat { get; set; }
        public DateTime operater_treat_time { get; set; }
        public int other_treat { get; set; }
        public DateTime other_treat_time { get; set; }
    }
}
