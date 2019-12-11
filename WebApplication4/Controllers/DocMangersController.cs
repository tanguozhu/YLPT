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
    public class DocMangersController : Controller
    {
        public string imglink;
        public DocMangerDbContext db = new DocMangerDbContext();
        //private DataTable dt = new DataTable();
        // GET: DocMangers
        public ActionResult Index(int page = 1)
        {
            List<DocManger> listdocmanger = new List<DocManger>();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select id,name,sex,position,goodat,introduction,phone,password,kind,isnew,Image from user where kind=1", mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        DocManger docmanger = new DocManger();
                        docmanger.Id = reader.GetInt32("id");
                       
                        docmanger.Name = reader.GetString("name");
                        docmanger.Sex = reader.GetInt32("sex");
                        docmanger.Position = reader.GetInt32("position");
                        string a;
                        a = reader.GetString("goodat");
                        docmanger.Goodat = getstring(a, 15);

                        string b;
                        b = reader.GetString("introduction");
                        docmanger.Introduction = getstring(b,15);
                        docmanger.Phone = reader.GetString("phone");
                        docmanger.Password = reader.GetString("password");
                        docmanger.Image = reader.GetString("Image");
                        listdocmanger.Add(docmanger);
                        
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
            var data = listdocmanger
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);
            return View(data);
            //return View(db.DocMangers.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Phone,string Name, int page = 1)
        {
            List<DocManger> listdocmanger = new List<DocManger>();
            MySqlConnection mysql = getMySqlConnection();
            string con;
            if (Phone == "" && Name == "")
            {
                con = "select * from user where kind=1";
            }
            else if (Phone != "" && Name != "")
            {
                con = "select * from user where kind=1 and (phone " +
                    "like" + "'%" + Phone + "%'" + "and name like " + "'%" + Name + "%')";
            }
            else if (Phone != "" && Name == "")
            {
                con = "select * from user where kind=1 and phone " +
                    "like" + "'%" + Phone + "%'";
            }
            else
            {
                con = "select * from user where kind=1 and " +
                    " name like " + "'%" + Name + "%'";
            }
            MySqlCommand mySqlCommand = getSqlCommand(con, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        DocManger docmanger = new DocManger();
                        docmanger.Id = reader.GetInt32("id");

                        docmanger.Name = reader.GetString("name");
                        docmanger.Sex = reader.GetInt32("sex");
                        docmanger.Position = reader.GetInt32("position");
                        string a;
                        a = reader.GetString("goodat");
                        docmanger.Goodat = getstring(a, 15);

                        string b;
                        b = reader.GetString("introduction");
                        docmanger.Introduction = getstring(b, 15);
                        docmanger.Phone = reader.GetString("phone");
                        docmanger.Password = reader.GetString("password");
                        docmanger.Image = reader.GetString("Image");
                        listdocmanger.Add(docmanger);
                        
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
            var data = listdocmanger
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);
            return View(data);
            //return View(db.DocMangers.ToList());
        }

        // GET: DocMangers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocManger docManger = new DocManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select id,name,sex,position,goodat,introduction,phone,password,kind,isnew,Image from user where kind=1 and id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        docManger.Id = reader.GetInt32("id");

                        docManger.Name = reader.GetString("name");
                        docManger.Sex = reader.GetInt32("sex");
                        docManger.Position = reader.GetInt32("position");
                       
                        docManger.Goodat = reader.GetString("goodat");
             
                        docManger.Introduction = reader.GetString("introduction");
                        docManger.Phone = reader.GetString("phone");
                        docManger.Password = reader.GetString("password");
                        docManger.Image = reader.GetString("Image");
                       
                    }
                }
                reader.Close();
            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                reader.Close();
            }
            if (docManger == null)
            {
                return HttpNotFound();
            }
            return View(docManger);
        }

        // GET: DocMangers/Create
        public ActionResult Create()
        {
            //ViewBag.getImageUrl = SaveImage();
            ViewBag.SexList = GetSexList();
            return View();
        }

        // POST: DocMangers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Sex,Phone,Password,Position,Goodat,Introduction,Image,Kind,Isnew,File")] DocManger docManger)
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
                result = "http://localhost:53028/" + imageStr + fileNames;// 设置数据库保存的路径

                docManger.Image = result;
            }
           
            
            //return result;
            
            //docManger.Image = SaveImage(file);
            //docManger.Image = SaveImage();
            docManger.Kind = "1";
            docManger.Isnew = "1";
            if (ModelState.IsValid)
            {
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("INSERT INTO user(name,sex,phone,password,position,goodat,introduction,Image,kind,isnew)VALUES" + "(" +
                "\"" + docManger.Name + "\"" + "," +  docManger.Sex  + "," + "\"" + docManger.Phone + "\""+ "," +
                "\"" + docManger.Password + "\""+ ","+  docManger.Position + ","+"\"" + docManger.Goodat + "\"" + "," +
                "\"" + docManger.Introduction + "\"" + "," + "\"" + docManger.Image + "\"" + "," + "\"" + docManger.Kind + "\"" + "," + "\"" + docManger.Isnew + "\"" + ")", mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Index");
            }
            else
            {
                List<string> Keys = ModelState.Keys.ToList();
                //获取每一个key对应的ModelStateDictionary
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述输出到控制台
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            ViewBag.SexList = GetSexList();
            return View(docManger);
        }

        // GET: DocMangers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocManger docManger = new DocManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select id,name,sex,position,goodat,introduction,phone,password,kind,isnew,Image from user where kind=1 and id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        docManger.Id = reader.GetInt32("id");

                        docManger.Name = reader.GetString("name");
                        docManger.Sex = reader.GetInt32("sex");
                        docManger.Position = reader.GetInt32("position");

                        docManger.Goodat = reader.GetString("goodat");

                        docManger.Introduction = reader.GetString("introduction");
                        docManger.Phone = reader.GetString("phone");
                        docManger.Password = reader.GetString("password");
                        docManger.Image = reader.GetString("Image");

                    }
                }
                reader.Close();
            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                reader.Close();
            }
            if (docManger == null)
            {
                return HttpNotFound();
            }
            ViewBag.SexList = GetSexList();
            return View(docManger);
        }

        // POST: DocMangers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( DocManger docManger)
        {
            string result = "";
            string getimage= docManger.Image;
            HttpPostedFileBase files = Request.Files["filename"];
            //docManger.File = Request.Files["filename"];
          
            string fileName = files.FileName;
            //Console.WriteLine(fileName);
            if (fileName=="")
            {
                docManger.Image = getimage;
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
                    result = "http://localhost:53028/" + imageStr + fileNames;// 设置数据库保存的路径

                    docManger.Image = result;
                }
            }
            if (ModelState.IsValid)
            {
                string a = Request.Form["Position"];
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand(" UPDATE user set name=" + 
                "\"" + docManger.Name + "\"" + ",sex=" +  docManger.Sex  + ",phone=" + "\"" + docManger.Phone + "\"" + ",password=" +
                "\"" + docManger.Password + "\"" + ",position=" +  docManger.Position + ",goodat=" + "\"" + docManger.Goodat + "\"" + ",introduction=" +
                "\"" + docManger.Introduction + "\"" + ",Image=" + "\"" + docManger.Image + "\"" +"where id=" +docManger.Id, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Index");
            }
            else
            {
                List<string> Keys = ModelState.Keys.ToList();
                //获取每一个key对应的ModelStateDictionary
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述输出到控制台
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }

            ViewBag.SexList = GetSexList();
            return View(docManger);
        }

        // GET: DocMangers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocManger docManger = new DocManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select id,name,sex,position,goodat,introduction,phone,password,kind,isnew,Image from user where kind=1 and id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        docManger.Id = reader.GetInt32("id");

                        docManger.Name = reader.GetString("name");
                        docManger.Sex = reader.GetInt32("sex");
                        docManger.Position = reader.GetInt32("position");

                        docManger.Goodat = reader.GetString("goodat");

                        docManger.Introduction = reader.GetString("introduction");
                        docManger.Phone = reader.GetString("phone");
                        docManger.Password = reader.GetString("password");
                        docManger.Image = reader.GetString("Image");

                    }
                }
                reader.Close();
            }
            catch
            {
                return HttpNotFound();
            }
            finally
            {
                reader.Close();
            }
            if (docManger == null)
            {
                return HttpNotFound();
            }
            return View(docManger);
        }

        // POST: DocMangers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocManger docManger = new DocManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("DELETE FROM user where id=" + id, mysql);
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

        public ActionResult PreViewing() {

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
