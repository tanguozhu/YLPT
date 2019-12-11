using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace WebApplication4.Models
{
    public class Visit
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
       
        public string Checkname;
        public int Checkpathology;
        public int refid;
        public int detailid;
        public DataTable getVisit;
        public DataTable getadverseraction;
        public DateTime dateTime;
        public DataTable dt;
        public DataTable getTreatment;
        public DataTable followup;
        public DataTable getGastroscopies;
        public DataTable pathology;
        public DataTable getTreatmentDetails;
        public DataTable weiniaomo;
    }

}
