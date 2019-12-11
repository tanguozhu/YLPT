using MySql.Data.MySqlClient;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            Login login = new Login();
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Account,string Password)
        {
            Login login = new Login();
            DataTable a;
            a = check(Account, Password);

            if (a.Rows.Count==0)
            {
                Response.Write("<script>alert('账号密码登录失败')</script>");
            }
            else
            {
               
                login.Name = a.Rows[0][3].ToString();
                login.webkind = Convert.ToInt32(a.Rows[0][4]);
                login.modify = Convert.ToInt32(a.Rows[0][5]);
                login.delete = Convert.ToInt32(a.Rows[0][6]);
                login.insert = Convert.ToInt32(a.Rows[0][7]);
                FormsAuthentication.RedirectFromLoginPage(login.Name+"."+login.webkind+"."+login.modify+"."+login.delete+"."+
                    login.insert,false);
                //Response.Write("<script>alert('账号密码登录失败')</script>");
            }
            
            return View(login);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public DataTable check(string account, string password)
        {
           // int a=0;
            DataTable dt = new DataTable();
            MySqlConnection mysql = getMySqlConnection();
            string sql = "select * from webmanger where account = " + "'" + account + "'" +
                "and password=" + "'" + password + "'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            
            command.Fill(dt);
          //  a = dt.Rows.Count;

            return dt;

        }

        private static MySqlConnection getMySqlConnection()
        {
            MySqlConnection mysql = new MySqlConnection(ConfigurationManager.ConnectionStrings["defalutconnection"].ConnectionString);
            return mysql;
        }
        public static MySqlCommand getSqlCommand(String sql, MySqlConnection mysql)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sql, mysql);

            return mySqlCommand;
        }
    }
}