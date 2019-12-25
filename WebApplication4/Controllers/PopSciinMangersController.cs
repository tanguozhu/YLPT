using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using PagedList;

namespace WebApplication4.Controllers
{
    public class PopSciinMangersController : Controller
    {
        private PopSciinMangerDbContext db = new PopSciinMangerDbContext();

        // GET: PopSciinMangers
        public ActionResult Index(int page = 1)
        {
            List<PopSciinManger> listpopSciinMangers = new List<PopSciinManger>();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from popsciinfortitle ", mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        PopSciinManger popSciinManger = new PopSciinManger();
                        popSciinManger.Id = reader.GetInt32("id");
                        string a;
                        a = reader.GetString("title");
                        popSciinManger.Title = getstring(a, 30);
                        popSciinManger.Imag = reader.GetString("imag");
                        listpopSciinMangers.Add(popSciinManger);

                    }
                }

            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                mysql.Close();
            }
            const int pagesize = 8;
            var data = listpopSciinMangers
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);
            return View(data);
            //return View(db.DocMangers.ToList());
        }

        // GET: PopSciinMangers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PopSciinManger popSciinManger = new PopSciinManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("SELECT * FROM  popsciinfortitle  WHERE id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        
                        popSciinManger.Id = reader.GetInt32("id");

                        popSciinManger.Title = reader.GetString("title");

                        popSciinManger.Imag = reader.GetString("imag");
                        string a= reader.GetString("introduction");
                        popSciinManger.Introduction = "<p>"+ a.Replace("\r\n", "</p><p>")+"</p>";
                       // popSciinManger.Introduction = reader.GetString("introduction");
                         
                        
                    }
                }

            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                mysql.Close();
            }

            return View(popSciinManger);
        }

        // GET: PopSciinMangers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PopSciinMangers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Imag,Introduction")] PopSciinManger popSciinManger)
        {
            string result = "";
            HttpPostedFileBase files = Request.Files["filename"];
            //docManger.File = Request.Files["filename"];
            string fileName = files.FileName;

            string fileFormat = fileName.Split('.')[fileName.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
            Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（这段可以限制上传文件类型）
            Console.WriteLine(Server.MapPath("~/"));

            if (string.IsNullOrEmpty(fileName) || !imageFormat.IsMatch(fileFormat)) // 验证后缀，判断文件是否是所要上传的格式
            {
                result = "error";
            }
            else
            {
                string timeStamp = DateTime.Now.Ticks.ToString(); // 获取当前时间的string类型
                string firstFileName = timeStamp.Substring(0, timeStamp.Length - 4); // 通过截取获得文件名
                string imageStr = "pic/"; // 获取保存附件的项目文件夹
                string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
                string pictureFormat = fileName.Split('.')[fileName.Split('.').Length - 1];// 设置文件格式
                string fileNames = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
                string saveFile = uploadPath + fileNames;//文件路径
                files.SaveAs(saveFile);// 保存文件
                                       // 如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！
                result = "http://58.192.132.31:9011/" + imageStr + fileNames;// 设置数据库保存的路径


            }
            popSciinManger.Imag = result;
            if (ModelState.IsValid)
            {
                
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("INSERT INTO popsciinfortitle(title,imag,introduction)VALUES" + "(" +
                "\"" + popSciinManger.Title + "\"" + "," + "\"" + popSciinManger.Imag  +"\""+"," + "\"" + popSciinManger.Introduction + "\""  + ")", mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Index");
            }

            return View(popSciinManger);
        }

        // GET: PopSciinMangers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PopSciinManger popSciinManger = new PopSciinManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("SELECT * FROM  popsciinfortitle  WHERE id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                        popSciinManger.Id = reader.GetInt32("id");
                        popSciinManger.Title = reader.GetString("title");
                        popSciinManger.Imag = reader.GetString("imag");
                        popSciinManger.Introduction = reader.GetString("introduction");

                    }
                }

            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                mysql.Close();
            }

            return View(popSciinManger);
        }

        // POST: PopSciinMangers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Imag,Introduction")] PopSciinManger popSciinManger)
        {
            string result = "";
            string getimage = popSciinManger.Imag;//将读出来的图片url先传给getimage
            HttpPostedFileBase files = Request.Files["filename"];
            //docManger.File = Request.Files["filename"];

            string fileName = files.FileName;
            //Console.WriteLine(fileName);
            if (fileName == "")
            {
                popSciinManger.Imag = getimage;//如果getimage值是空的，就将原来的值还给Imag
            }
            else
            {
                string fileFormat = fileName.Split('.')[fileName.Split('.').Length - 1]; // 以“.”截取，获取“.”后面的文件后缀
                Regex imageFormat = new Regex(@"^(bmp)|(png)|(gif)|(jpg)|(jpeg)"); // 验证文件后缀的表达式（这段可以限制上传文件类型）
                Console.WriteLine(Server.MapPath("~/"));

                if (string.IsNullOrEmpty(fileName) || !imageFormat.IsMatch(fileFormat)) // 验证后缀，判断文件是否是所要上传的格式
                {
                    result = "error";
                }
                else
                {
                    string timeStamp = DateTime.Now.Ticks.ToString(); // 获取当前时间的string类型
                    string firstFileName = timeStamp.Substring(0, timeStamp.Length - 4); // 通过截取获得文件名
                    string imageStr = "pic/"; // 获取保存附件的项目文件夹
                    string uploadPath = Server.MapPath("~/" + imageStr); // 将项目路径与文件夹合并
                    string pictureFormat = fileName.Split('.')[fileName.Split('.').Length - 1];// 设置文件格式
                    string fileNames = firstFileName + "." + fileFormat;// 设置完整（文件名+文件格式） 
                    string saveFile = uploadPath + fileNames;//文件路径
                    files.SaveAs(saveFile);// 保存文件
                                           // 如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！
                    result = "http://58.192.132.31:9011/" + imageStr + fileNames;// 设置数据库保存的路径

                    popSciinManger.Imag = result;//将新的图片地址传给Imag
                }
            }
            if (ModelState.IsValid)
            {
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("UPDATE popsciinfortitle set title=" +
                "'" + popSciinManger.Title + "'" + ",introduction=" + "'" + popSciinManger.Introduction + "'" +
                ",Imag=" + "'" + popSciinManger.Imag + "'" + "where id="+popSciinManger.Id, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Index");
            }
            return View(popSciinManger);
        }

        // GET: PopSciinMangers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PopSciinManger popSciinManger = new PopSciinManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("SELECT * FROM  popsciinfortitle  WHERE id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                        popSciinManger.Id = reader.GetInt32("id");

                        popSciinManger.Title = reader.GetString("title");

                        popSciinManger.Imag = reader.GetString("imag");
                        string a = reader.GetString("introduction");
                        popSciinManger.Introduction = "<p>" + a.Replace("\r\n", "</p><p>") + "</p>";

                    }
                }

            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                mysql.Close();
            }

            return View(popSciinManger);
        }

        // POST: PopSciinMangers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PopSciinManger popSciinManger = new PopSciinManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("DELETE FROM popsciinfortitle where id=" + id, mysql);
            mysql.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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

        public static string getstring(string str, int length)
        {
            int i = 0, j = 0;
            foreach (char chr in str)
            {
                if ((int)chr > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }
                if (i > length)
                {
                    str = str.Substring(0, j) + "...";
                    break;
                }
                j++;
            }
            return str;

        }

        public ActionResult PreViewing()
        {

            return View();
        }

        private List<SelectListItem> GetSexList()
        {
            return new List<SelectListItem>() {
              new SelectListItem
              {
                     Text = "男",
                     Value = "1"
              },new SelectListItem
              {
                     Text = "女",
                     Value = "0"
              }
       };
        }
    }
}
