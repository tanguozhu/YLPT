using System;
using System.Web;

namespace WebApplication4.Models
{
    public class Login 
    {
       public string Name { get; set; }
       public string Account { get; set;}
       public string PassWord { get; set; }
       public int webkind { get; set; }
       public int modify { get; set; }
       public int delete { get; set; }
       public int insert { get; set; }
       public int daochu { get; set; }
    }
}
