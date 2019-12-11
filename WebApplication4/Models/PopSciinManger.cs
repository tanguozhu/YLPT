using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication4.Models
{
    public class PopSciinManger 
    {
        public int Id { get; set; }
        [Display(Name = "标题")]
        [Required]
        [StringLength(300)]
        public string Title { get; set; }
        [Display(Name = "图片")]
        [Required]
        [StringLength(300)]
        public string Imag { get; set; }
        [Display(Name = "文字")]
        [StringLength(3000)]
        public string Introduction { get; set; }
        
    }
}
