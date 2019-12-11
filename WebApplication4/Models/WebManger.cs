using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebApplication4.Models
{
    public class WebManger 
    {
        public int Id { get; set; }
        
        [Display(Name = "账号")]
        [Required]
        [StringLength(255)]
        public string Account { get; set; }

        [Display(Name = "密码")]
        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Display(Name = "姓名")]
        [Required]
        [StringLength(255, MinimumLength = 2)]
        public string Name { get; set; }

        public int webkind { get; set; }
        public int modify { get; set; }
        public int delete { get; set; }
        public int insert { get; set; }
    }
}
