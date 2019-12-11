using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApplication4.Models
{
    public class Student 
    {
        public int ID { get; set; }

        [Display(Name = "姓名")]
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Name { get; set; }


        public string outputexcel;
        [Display(Name = "性别")]
        [Required]
        [Range(0, 1)]
        public int Gender { get; set; }

        [Display(Name = "所学专业")]
        [Required]
        [StringLength(200)]
        public string Major { get; set; }

        [Display(Name = "入学日期")]
       [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EntranceDate { get; set; }
    }
}
