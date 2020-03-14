using MySql.Data.MySqlClient;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
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

        public void Button_Up_Click()
        {
            string fileName = "扬大附院消化科室.apk";//客户端保存的文件名
            string imageStr = "pic/"; // 获取保存附件的项目文件夹
            //string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
            string filePath = Server.MapPath("~/" + imageStr+ fileName);//路径

            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
            // ExportDataToExcel(dt, path1, ht, strTitle, sheetName1, xlsName);

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
                login.daochu = Convert.ToInt32(a.Rows[0][8]);
                FormsAuthentication.RedirectFromLoginPage(login.Name+"."+login.webkind+"."+login.modify+"."+login.delete+"."+
                    login.insert + "." + login.daochu, false);
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

            Button_Up_Click();
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