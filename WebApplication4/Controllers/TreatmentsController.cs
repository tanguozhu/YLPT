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
    public class TreatmentsController : Controller
    {
        private TreatmentDbContext db = new TreatmentDbContext();

        // GET: Treatments
        public ActionResult Index()
        {
            return View(db.Treatments.ToList());
        }

        // GET: Treatments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // GET: Treatments/Create
        public ActionResult Create(int? id, string scannum,string visit,int refid,int name)
        {
            Treatment treatment = new Treatment();
            treatment.scannum = scannum;
            treatment.visit = visit;
            treatment.refId = refid;
            treatment.name = name;
            if (name == 0)
            {
                //treatment.content = "1";
                //treatment.isEradicated = 1;
            }
            else
            {
                treatment.condition = "1";
                // treatment.isInfected = 0;
                //treatment.isEradicated = 0;
                treatment.Omeprazole = 0;
                treatment.isEradicated = 2;
                treatment.Rabeprazole = 0;
                treatment.Esomeprazole = 0;
                treatment.Pantoprazole = 0;
                treatment.OtherPPI = 0;
                treatment.Amoxicillin = 0;
                treatment.tetracycline = 0;
                treatment.Levofloxacin = 0;
                treatment.Clarithromycin = 0;
                treatment.Furazolidone = 0;
                treatment.Metronidazole = 0;
                
            }
            return View(treatment);
        }

        // POST: Treatments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Treatment treatment)
        {
            if (treatment.name == 2)
            {

                string result = "";
                string result1 = "";
                string result2 = "";

                HttpPostedFileBase files = Request.Files["filename"];
                HttpPostedFileBase files1 = Request.Files["filename1"];
                HttpPostedFileBase files2 = Request.Files["filename2"];


                result = SaveImage(files);
                result1 = SaveImage(files1);
                result2 = SaveImage(files2);


                if (result != "error")
                {
                    treatment.pic1 = result;
                }

                if (result1 != "error")
                {
                    treatment.pic2 = result1;
                }
                if (result2 != "error")
                {
                    treatment.pic3 = result2;
                }
            }
			
			
			if (ModelState.IsValid)
            {
				
				string a = Request.Form["refId"];
				//string c= Request.Form["content"];
				int b = Convert.ToInt32(a);
                string sql = "";
                if (treatment.name==0)
                {
                    sql = " INSERT INTO treatment (visit,scannum,name,conditions,time,Omeprazole,Rabeprazole,Esomeprazole," +
                        "Pantoprazole,OtherPPI,biji,Amoxicillin,tetracycline,Levofloxacin,Clarithromycin,Furazolidone,Metronidazole,treatTime,isOnTime,isEradicated,isfollowup,content)VALUES(" + 
                        "'" + treatment.visit + "','" + treatment.scannum + "'," +treatment.name  + ",'"+treatment.condition+"','"+treatment.time+"'," +
						"" + treatment.Omeprazole + "," + treatment.Rabeprazole + "," + treatment.Esomeprazole + ","+treatment.Pantoprazole + "," +
	  "" + treatment.OtherPPI + ","+treatment.biji+"," + treatment.Amoxicillin + "," + treatment.tetracycline + "," + treatment.Levofloxacin + "," +
   ""+treatment.Clarithromycin + "," + treatment.Furazolidone + "," + treatment.Metronidazole + "," + treatment.treatTime + "," + treatment.isOnTime + "," + 
   treatment.isEradicated + ",0 ,'"+treatment.content+"')";
                }
				else if (treatment.name == 1)
				{
                    sql = " INSERT INTO treatment (visit,scannum,name,time,treatTime,isOnTime,content)VALUES(" +
                        "'" + treatment.visit + "','" + treatment.scannum + "'," + treatment.name + ",'"  + treatment.time + "'," +
                          treatment.treatTime + "," + treatment.isOnTime + ",'"+treatment.content+"' )";
                }
				else 
				{
					sql = " INSERT INTO treatment (visit,name,scannum,conditions,time,content,pic1,pic2,pic3,isfollowup)VALUES(" +
						"'" + treatment.visit + "',"+ treatment.name+",'" + treatment.scannum + "','" + treatment.condition + "','" + treatment.time + "','" +
						  treatment.content + "','" + treatment.pic1 + "','" + treatment.pic2 + "','" + treatment.pic3 + "'," + treatment.isfollowup + " )";
				}
				MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                
                return RedirectToAction("Details/" + b, "Visits");
               
                
            }

            return View(treatment);
        }

        // GET: Treatments/Edit/5
        public ActionResult Edit(int? id,int refid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = new Treatment();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from treatment where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        treatment.Id = reader.GetInt32("Id");
                        treatment.refId = refid;
                        treatment.visit = reader.GetString("visit");
                        treatment.scannum = reader.GetString("scannum");
                        treatment.name = reader.GetInt32("name");
                        treatment.condition = reader.GetString("conditions");
                        treatment.time = reader.GetDateTime("time");

                        //treatment.isInfected = reader.GetInt32("isInfected");
                        treatment.isEradicated = reader.GetInt32("isEradicated");
                        treatment.Omeprazole = reader.GetInt32("Omeprazole");
                        treatment.Rabeprazole = reader.GetInt32("Rabeprazole");
                        treatment.Esomeprazole = reader.GetInt32("Esomeprazole");
                        treatment.Pantoprazole = reader.GetInt32("Pantoprazole");
                        treatment.OtherPPI = reader.GetInt32("OtherPPI");
                        treatment.Amoxicillin = reader.GetInt32("Amoxicillin");
                        treatment.tetracycline = reader.GetInt32("tetracycline");
                        treatment.Levofloxacin = reader.GetInt32("Levofloxacin");
                        treatment.Clarithromycin = reader.GetInt32("Clarithromycin");
                        treatment.Furazolidone = reader.GetInt32("Furazolidone");
                        treatment.Metronidazole = reader.GetInt32("Metronidazole");
                        treatment.treatTime = reader.GetInt32("treatTime");
                        treatment.isOnTime = reader.GetInt32("isOnTime");
						treatment.biji = reader.GetInt32("biji");
						treatment.isfollowup = reader.GetInt32("isfollowup");
						
						treatment.content = reader.GetString("content");
						treatment.pic1 = reader.GetString("pic1");
						treatment.pic2 = reader.GetString("pic2");
						treatment.pic3 = reader.GetString("pic3");
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
            return View(treatment);
        }

        // POST: Treatments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                string a = Request.Form["refId"];
                int b = Convert.ToInt32(a);
                int checkname = treatment.name;
                string sql = "";
                if (checkname==0)
                {
					sql = "UPDATE treatment set " +
						"conditions='" + treatment.condition + "',time='" + treatment.time + "',Omeprazole=" + treatment.Omeprazole + ",Omeprazole=" + treatment.Omeprazole + ",Rabeprazole=" + treatment.Rabeprazole + "," +
	  "Esomeprazole=" + treatment.Esomeprazole + ",Pantoprazole=" + treatment.Pantoprazole + ",OtherPPI=" + treatment.OtherPPI + ",Amoxicillin=" + treatment.Amoxicillin + ",tetracycline=" + treatment.tetracycline + "," +
   "Levofloxacin=" + treatment.Levofloxacin + ",Clarithromycin=" + treatment.Clarithromycin + ",Furazolidone=" + treatment.Furazolidone + ",Metronidazole=" + treatment.Metronidazole + ",treatTime=" + treatment.treatTime + "," +
   "isOnTime=" + treatment.isOnTime + ",isfollowup=" + treatment.isfollowup + ",biji=" + treatment.biji + ",isEradicated=" + treatment.isEradicated + ",content='"+treatment.content+"' where id=" + treatment.Id + "";
					//sql = "UPDATE treatment set conditions='"+treatment.condition+"',time='"+treatment.time+ "',isInfected="+treatment.isInfected+ ",isEradicated="+
     //                   treatment.isEradicated + ",Omeprazole=" + treatment.Omeprazole + ",Omeprazole=" + treatment.Omeprazole + ",Rabeprazole=" + treatment.Rabeprazole +
     //                   ",Esomeprazole=" + treatment.Esomeprazole+ ",Pantoprazole=" + treatment.Pantoprazole + ",OtherPPI=" + treatment.OtherPPI + ",Amoxicillin=" + treatment.Amoxicillin+
     //                   ",tetracycline=" + treatment.tetracycline+ ",Levofloxacin=" + treatment.Levofloxacin + ",Clarithromycin=" + treatment.Clarithromycin +
     //                   ",Furazolidone=" + treatment.Furazolidone+ ",Metronidazole=" + treatment.Metronidazole + ",treatTime=" + treatment.treatTime + ",isOnTime=" + treatment.isOnTime+
     //                   " where id="+ treatment.Id;
                }
                else if (checkname ==1)
				{
                    sql= "UPDATE treatment set time='" + treatment.time +  "',treatTime=" + treatment.treatTime + ",isOnTime=" + treatment.isOnTime+
                        ",content='" + treatment.content+"' where id="+treatment.Id;
                }
				else 
				{
					sql = "UPDATE treatment set time='" + treatment.time + "',pic1=" + treatment.pic1 + ",pic2=" + treatment.pic2 + ",pic3=" + treatment.pic3 + "," +
						"isfollowup=" + treatment.isfollowup +",content='" + treatment.content + "',condition='" + treatment.condition+"' where id=" + treatment.Id;
				}
				MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
               
                return RedirectToAction("Details/" + b, "Visits");
            }
            return View(treatment);
        }

        // GET: Treatments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: Treatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            db.Treatments.Remove(treatment);
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
