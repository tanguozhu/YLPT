using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace WebApplication4.Models
{
    public class UserManger 
    {
        public int Id { get; set; }

        [Display(Name = "姓名")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "密码")]
        [StringLength(100)]
        public string Password { get; set; }

        [Display(Name = "手机号")]
        [StringLength(100)]
        public string Phone { get; set; }

        [Display(Name = "筛查号")]
        public string scannum { get; set; }

       

        [Display(Name = "身份证号")]
        public string Identinum { get; set; }
        public int checkidentinum=2;


        [Display(Name = "昵称")]
        public string pnickname { get; set; }
        public string Kind { get; set; }
        public string Isnew { get; set; }
        [Display(Name = "图片")]
        [StringLength(300)]
        public string Image { get; set; }
       

        public int isverifyId = 0;

    }
}
