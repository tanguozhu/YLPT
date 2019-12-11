using MySql.Data.MySqlClient;
using PagedList;
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

namespace WebApplication4.Controllers
{
    public class BriefIntroesController : Controller
    {
        private BriefIntroDbContext db = new BriefIntroDbContext();

        // GET: BriefIntroes
        public ActionResult Index(int id=1)
        {
            
            BriefIntro briefIntro = new BriefIntro();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from briefintroofdepartment where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        briefIntro.Id = reader.GetInt32("id");
                        briefIntro.Introduction = reader.GetString("introduction");

                        briefIntro.Pic = reader.GetString("pic");
                       

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
            if (briefIntro == null)
            {
                return HttpNotFound();
            }
            return View(briefIntro);
        }

        // GET: BriefIntroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BriefIntro briefIntro = db.BriefIntros.Find(id);
            if (briefIntro == null)
            {
                return HttpNotFound();
            }
            return View(briefIntro);
        }

        // GET: BriefIntroes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BriefIntroes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Introduction,Pic")] BriefIntro briefIntro)
        {
            if (ModelState.IsValid)
            {
                db.BriefIntros.Add(briefIntro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(briefIntro);
        }

        // GET: BriefIntroes/Edit/5
        public ActionResult Edit(int id=1)
        {
           
            BriefIntro briefIntro = new BriefIntro();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from briefintroofdepartment where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        briefIntro.Id = reader.GetInt32("id");
                        string a = 
                        briefIntro.Introduction = reader.GetString("introduction");
                        briefIntro.Pic = reader.GetString("pic");


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
            if (briefIntro == null)
            {
                return HttpNotFound();
            }
            return View(briefIntro);
        }

        // POST: BriefIntroes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Introduction,Pic")] BriefIntro briefIntro)
        {
            string result = "";
            string getimage = briefIntro.Pic;
            HttpPostedFileBase files = Request.Files["filename"];
            string fileName = files.FileName;
            //Console.WriteLine(fileName);
            if (fileName == "")
            {
                briefIntro.Pic = getimage;
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
                    files.SaveAs(saveFile);
                    // 如果单单是上传，不用保存路径的话，下面这行代码就不需要写了！

                    result = "http://localhost:53028/" + imageStr + fileNames;// 设置数据库保存的路径

                    

                    briefIntro.Pic = result;
                }
            }
            if (ModelState.IsValid)
            {
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("UPDATE briefintroofdepartment set introduction=" +
                "\"" + briefIntro.Introduction + "\"" + ",pic=" + "\"" + briefIntro.Pic + "\"" +  "where id=" + briefIntro.Id, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Index");
            }
            return View(briefIntro);
        }

        // GET: BriefIntroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BriefIntro briefIntro = db.BriefIntros.Find(id);
            if (briefIntro == null)
            {
                return HttpNotFound();
            }
            return View(briefIntro);
        }

        // POST: BriefIntroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BriefIntro briefIntro = db.BriefIntros.Find(id);
            db.BriefIntros.Remove(briefIntro);
            db.SaveChanges();
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
    }
}
