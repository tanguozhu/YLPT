using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApplication4.Models
{
    public class DocManger 
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "性别")]
        [Required]
        [Range(0, 1)]
        public int Sex { get; set; }

        [Display(Name = "职位")]
        public int Position { get; set; }

        [Display(Name = "擅长")]
       
        [StringLength(255)]
        public string Goodat { get; set; }

        [Display(Name = "介绍")]
        
        [StringLength(1000)]
        public string Introduction { get; set; }


        [Display(Name = "手机号")]
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Phone { get; set; }

        [Display(Name = "密码")]
        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public string Kind { get; set; }

        public string gonghao { get; set; }

        public string danwei { get; set; }


        public string Isnew { get; set; }

        [Display(Name = "图片")]
        
        public string Image { get; set; }
        
       
        //public HttpPostedFileBase File { get; set; }
    }
}
