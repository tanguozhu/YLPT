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
    public class FollowUpsController : Controller
    {
        private FollowUpDbContext db = new FollowUpDbContext();

        // GET: FollowUps
        public ActionResult Index()
        {
            return View(db.FollowUps.ToList());
        }

        // GET: FollowUps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowUp followUp = new FollowUp();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from followup where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        followUp.Id = reader.GetInt32("id");
                        followUp.visit = reader.GetString("visit");
                        
                        followUp.scannum = reader.GetString("scannum");

                        followUp.date = reader.GetDateTime("date");
                        followUp.content = reader.GetString("content");
                       
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
            return View(followUp);
        }

        // GET: FollowUps/Create
        public ActionResult Create(int?id,string scannum,int refid,string visit)
        {
            FollowUp followUp = new FollowUp();
            followUp.scannum = scannum;
            followUp.refid = refid;
            followUp.visit = visit;
            return View(followUp);
        }

        // POST: FollowUps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( FollowUp followUp)
        {

            string a = Request.Form["refid"];
            string b = Request.Form["visit"];
            string c = Request.Form["scannum"];
            //插入gas表
            string dt = Request.Form["dt"];
            string Checknum = Request.Form["Checknum"];
            string Conclusion = Request.Form["Conclusion"];
            string Pathologynum = Request.Form["Pathologynum"];
            string Pathologyconclusion = Request.Form["Pathologyconclusion"];
            string Other = Request.Form["Other"];
            {
                string result = "";
                string result1 = "";
                string result2 = "";
                string result3 = "";
                string result4 = "";
                string result5 = "";
                string result6 = "";
                string result7 = "";
                string pic1 = "";
                string pic2 = "";
                string pic3 = "";
                string pic4 = "";
                string pic5 = "";
                string pic6 = "";
                string pic7 = "";
                string pic8 = "";
                HttpPostedFileBase files = Request.Files["filename"];
                HttpPostedFileBase files1 = Request.Files["filename1"];
                HttpPostedFileBase files2 = Request.Files["filename2"];
                HttpPostedFileBase files3 = Request.Files["filename3"];
                HttpPostedFileBase files4 = Request.Files["filename4"];
                HttpPostedFileBase files5 = Request.Files["filename5"];
                HttpPostedFileBase files6 = Request.Files["filename6"];
                HttpPostedFileBase files7 = Request.Files["filename7"];

                result = SaveImage(files);
                result1 = SaveImage(files1);
                result2 = SaveImage(files2);
                result3 = SaveImage(files3);
                result4 = SaveImage(files4);
                result5 = SaveImage(files5);
                result6 = SaveImage(files6);
                result7 = SaveImage(files7);

                if (result != "error")
                {
                    pic1 = result;
                }


                if (result1 != "error")
                {
                    pic2 = result1;
                }
                if (result2 != "error")
                {
                    pic3 = result2;
                }
                if (result3 != "error")
                {
                    pic4 = result3;
                }
                if (result4 != "error")
                {
                    pic5 = result4;
                }
                if (result5 != "error")
                {
                    pic6 = result5;
                }
                if (result6 != "error")
                {
                    pic7 = result6;
                }
                if (result7 != "error")
                {
                    pic8 = result7;
                }
            }


            //插入tre表
            string time1 = Request.Form["time1"];
            string condition = Request.Form["condition"];
            string content1 = Request.Form["content1"];
            {
                string results = "";
                string results1 = "";
                string results2 = "";
                string pics1 = "";
                string pics2 = "";
                string pics3 = "";


                HttpPostedFileBase filess = Request.Files["filenames"];
                HttpPostedFileBase filess1 = Request.Files["filenames1"];
                HttpPostedFileBase filess2 = Request.Files["filenames2"];


                results = SaveImage(filess);
                results1 = SaveImage(filess1);
                results2 = SaveImage(filess2);


                if (results != "error")
                {
                    pics1 = results;
                }

                if (results1 != "error")
                {
                    pics2 = results1;
                }
                if (results2 != "error")
                {
                    pics3 = results2;
                }
            }

            //gaschecknum获取

            //treid获取

            //gasid获取

            if (ModelState.IsValid)
            {
               
                MySqlConnection mysql = getMySqlConnection();
                string sql = "";
                sql = "INSERT INTO followup (visit,scannum,date,nextdate,followupnum,gaschecknum,treid,gasid)VALUES('" + b+"','" + c + "','" +
                    followUp.date + "'," + "'" + followUp.nextdate + "',"+followUp.followupnum+",'"+followUp.gaschecknum+
                    "',"+followUp.treid+","+followUp.gasid+")";
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/"+Convert.ToInt32(a), "Visits");
            }

            return View(followUp);
        }

        // GET: FollowUps/Edit/5
        public ActionResult Edit(int? id,int refid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowUp followUp = new FollowUp();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from followup where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        followUp.refid = refid;
                        followUp.Id = reader.GetInt32("id");
                        followUp.visit = reader.GetString("visit");

                        followUp.scannum = reader.GetString("scannum");

                        followUp.date = reader.GetDateTime("date");
                        followUp.content = reader.GetString("content");

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
            return View(followUp);
        }

        // POST: FollowUps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( FollowUp followUp)
        {
            if (ModelState.IsValid)
            {
                string a = Request.Form["refid"];
                string b = Request.Form["visit"];
                string c = Request.Form["scannum"];
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand(" UPDATE followup set date=" + "'" + followUp.date + "',content=" +
               "'" + followUp.content + "'" + "where id=" + followUp.Id, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/" + Convert.ToInt32(a), "Visits");
            }
            return View(followUp);
        }

        // GET: FollowUps/Delete/5
        public ActionResult Delete(int? id,int refid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FollowUp followUp = new FollowUp();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from followup where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        followUp.refid = refid;
                        followUp.Id = reader.GetInt32("id");
                        followUp.visit = reader.GetString("visit");

                        followUp.scannum = reader.GetString("scannum");

                        followUp.date = reader.GetDateTime("date");
                        followUp.content = reader.GetString("content");

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
            return View(followUp);
        }

        // POST: FollowUps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,int refid)
        {
            int a = refid;
            
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("DELETE FROM followup where id=" + id, mysql);
            mysql.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            return RedirectToAction("Details/" + Convert.ToInt32(a), "Visits");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private void Insertgas(string checkname, string scannum, string visit, string checktime, string checknum, string conclusion, 
            string pathologynum, string pathologyconclusion, string other, string Pic1, string Pic2, string Pic3, string Pic4,
            string Pic5, string Pic6, string Pic7, string Pic8,int followupnum)
        {

            MySqlConnection mysql = getMySqlConnection();
            string sql = " INSERT INTO gastroscopy (checkname,scannum,visit,checktime,checknum,conclusion,pathologynum,pathologyconclusion,other,pic1,pic2,pic3,pic4,pic5," +
                "pic6,pic7,pic8,isfollowup,followupnum)" +
                    "VALUES('" + checkname + "','" + scannum + "','" + visit + "','" + checktime + "','" + checknum + "','" + conclusion + "','" + pathologynum +
                    "','" + pathologyconclusion + "','" + other + "','" + Pic1 + "','" + Pic2 + "','" +
                    Pic3 + "','" + Pic4 + "','" + Pic5 + "','" + Pic6 + "','" +
                    Pic7 + "','" + Pic8 + "'," + 1 + followupnum + " )";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();

        }

        private void Inserttre(string visit, string name, string scannum, string conditions, string time, string content,
            string pic1, string pic2, string pic3, int followupnum)
        {

            MySqlConnection mysql = getMySqlConnection();
            string sql = " INSERT INTO treatment (visit,name,scannum,conditions,time,content,pic1,pic2,pic3," +
                "isfollowup,followupnum)VALUES(" +
                        "'" + visit + "'," + name + ",'" + scannum + "','" + conditions + "','" + time + "','" +
                          content + "','" + pic1 + "','" + pic2 + "','" + pic3 + "'," + 1 + followupnum + " )";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();

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
        //保存图片
        public string SaveImage(HttpPostedFileBase getfile)
        {
            string result = "";
            HttpPostedFileBase files = getfile;
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


            }
            return result;
        }
    }
}
