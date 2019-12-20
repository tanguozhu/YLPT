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
    public class GastroscopiesController : Controller
    {
        private GastroscopyDbContext db = new GastroscopyDbContext();

        // GET: Gastroscopies
        public ActionResult Index(int id,string name)
        {
            return View(db.Gastroscopies.ToList());
        }

        // GET: Gastroscopies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gastroscopy gastroscopy = db.Gastroscopies.Find(id);
            if (gastroscopy == null)
            {
                return HttpNotFound();
            }
            return View(gastroscopy);
        }

        // GET: Gastroscopies/Create
        public ActionResult Create(int?id,string scannum,string visit,int refid)
        {
            Gastroscopy gastroscopy = new Gastroscopy();
            gastroscopy.PastId = refid;
            gastroscopy.scannum = scannum;
            gastroscopy.Visit = visit;
            return View(gastroscopy);
        }

        // POST: Gastroscopies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Checktime,Visit,scannum,Other,Checkname,Checktime," +
            "Checknum,Conclusion,Pathologynum,Pathologyconclusion,Pic1,Pic2,Pic3,Pic4,Pic5")] Gastroscopy gastroscopy)
        {
            string result = "";
            string result1 = "";
            string result2 = "";
            string result3 = "";
            string result4 = "";
            string result5 = "";
            string result6 = "";
            string result7 = "";
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
                gastroscopy.Pic1 = result;
            }

            if (result1 != "error")
            {
                gastroscopy.Pic2 = result1;
            }
            if (result2 != "error")
            {
                gastroscopy.Pic3 = result2;
            }
            if (result3 != "error")
            {
                gastroscopy.Pic4 = result3;
            }
            if (result4 != "error")
            {
                gastroscopy.Pic5 = result4;
            }
            if (result5 != "error")
            {
                gastroscopy.Pic6 = result5;
            }
            if (result6 != "error")
            {
                gastroscopy.Pic7 = result6;
            }
            if (result7 != "error")
            {
                gastroscopy.Pic8 = result7;
            }


            if (ModelState.IsValid)
            {
                string a = Request.Form["scannum"];
                string b = Request.Form["Visit"];
                string c = "胃镜检查";
                string d = Request.Form["dt"];
                string e = Request.Form["PastId"];



                MySqlConnection mysql = getMySqlConnection();
                string sql = "";
                if (gastroscopy.Pic1=="1")
                {
                    gastroscopy.Pic1 = "";
                }
                if (gastroscopy.Pic2 == "1")
                {
                    gastroscopy.Pic2 = "";
                }
                if (gastroscopy.Pic3 == "1")
                {
                    gastroscopy.Pic3 = "";
                }
                if (gastroscopy.Pic4 == "1")
                {
                    gastroscopy.Pic4 = "";
                }
                if (gastroscopy.Pic5 == "1")
                {
                    gastroscopy.Pic5 = "";
                }
                if (gastroscopy.Pic6 == "1")
                {
                    gastroscopy.Pic6 = "";
                }
                if (gastroscopy.Pic7 == "1")
                {
                    gastroscopy.Pic7 = "";
                }
                if (gastroscopy.Pic8 == "1")
                {
                    gastroscopy.Pic8 = "";
                }

                sql = " INSERT INTO gastroscopy (checkname,scannum,visit,checktime,checknum,conclusion,pathologynum,pathologyconclusion,other,pic1,pic2,pic3,pic4,pic5,pic6,pic7,pic8,isfollowup)" +
                    "VALUES('" +c+"','"+ a + "','" + b + "','" + d + "','" + gastroscopy.Checknum + "','" + gastroscopy.Conclusion + "','" + gastroscopy.Pathologynum +
                    "','" + gastroscopy.Pathologyconclusion + "','" + gastroscopy.Other + "','" + gastroscopy.Pic1 + "','" + gastroscopy.Pic2 + "','" +
                    gastroscopy.Pic3 + "','" + gastroscopy.Pic4 + "','" + gastroscopy.Pic5 + "','" + gastroscopy.Pic6 + "','" + 
                    gastroscopy.Pic7 + "','" + gastroscopy.Pic8+ "',"+ 0 + " )";
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/"+e, "Visits");
            }

            return View(gastroscopy);
        }


        // GET: Gastroscopies/Create
        public ActionResult CreateOther(int? id, string scannum, string visit, int refid)
        {
            Gastroscopy gastroscopy = new Gastroscopy();
            gastroscopy.PastId = refid;
            gastroscopy.scannum = scannum;
            gastroscopy.Visit = visit;
            return View(gastroscopy);
        }

        // POST: Gastroscopies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOther([Bind(Include = "Id,Checktime,Visit,scannum,Other,Checkname,Checktime," +
            "Checknum,Conclusion,Pathologynum,Pathologyconclusion,Pic1,Pic2,Pic3,Pic4,Pic5")] Gastroscopy gastroscopy)
        {
            string result = "";
            string result1 = "";
            string result2 = "";
            string result3 = "";
            string result4 = "";
            HttpPostedFileBase files = Request.Files["filename"];
            HttpPostedFileBase files1 = Request.Files["filename1"];
            HttpPostedFileBase files2 = Request.Files["filename2"];
            HttpPostedFileBase files3 = Request.Files["filename3"];
            HttpPostedFileBase files4 = Request.Files["filename4"];

            result = SaveImage(files);
            result1 = SaveImage(files1);
            result2 = SaveImage(files2);
            result3 = SaveImage(files3);
            result4 = SaveImage(files4);

            if (result != "error")
            {
                gastroscopy.Pic1 = result;
            }

            if (result1 != "error")
            {
                gastroscopy.Pic2 = result1;
            }
            if (result2 != "error")
            {
                gastroscopy.Pic3 = result2;
            }
            if (result3 != "error")
            {
                gastroscopy.Pic4 = result3;
            }
            if (result4 != "error")
            {
                gastroscopy.Pic2 = result4;
            }


            if (ModelState.IsValid)
            {
                string a = Request.Form["scannum"];
                string b = Request.Form["Visit"];
               
                string d = Request.Form["dt"];
                string e = Request.Form["PastId"];

               string g= Request.Form["Checkname"];


                MySqlConnection mysql = getMySqlConnection();
                string sql = "";
                if (gastroscopy.Pic1 == "1")
                {
                    gastroscopy.Pic1 = "";
                }
                if (gastroscopy.Pic2 == "1")
                {
                    gastroscopy.Pic2 = "";
                }
                if (gastroscopy.Pic3 == "1")
                {
                    gastroscopy.Pic3 = "";
                }
                if (gastroscopy.Pic4 == "1")
                {
                    gastroscopy.Pic4 = "";
                }
                if (gastroscopy.Pic5 == "1")
                {
                    gastroscopy.Pic5 = "";
                }

                sql = " INSERT INTO gastroscopy (checkname,scannum,visit,checktime,checknum,conclusion,pic1,pic2,pic3,pic4,pic5)" +
                    "VALUES('" + gastroscopy.Checkname + "','" + a + "','" + b + "','" + d + "','" + gastroscopy.Checknum + "','" + gastroscopy.Conclusion + "','"  + gastroscopy.Pic1 + "','" + gastroscopy.Pic2 + "','" +
                    gastroscopy.Pic3 + "','" + gastroscopy.Pic4 + "','" + gastroscopy.Pic5 + "')";
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/" + e, "Visits");
            }

            return View(gastroscopy);
        }


        // GET: Gastroscopies/Create
        public ActionResult Createyoumen(int? id, string scannum, string visit, int refid)
        {
            Gastroscopy gastroscopy = new Gastroscopy();
            gastroscopy.PastId = refid;
            gastroscopy.scannum = scannum;
            gastroscopy.Visit = visit;
            return View(gastroscopy);
        }

        // POST: Gastroscopies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createyoumen( Gastroscopy gastroscopy)
        {
            

            if (ModelState.IsValid)
            {
                string a = Request.Form["scannum"];
                string b = Request.Form["Visit"];

                string d = Request.Form["dt"];
                string e = Request.Form["PastId"];

                string g = "幽门螺杆菌检查";
                string f = Request.Form["isInfected"];//结果

                MySqlConnection mysql = getMySqlConnection();
                string sql = "";
               

                sql = " INSERT INTO gastroscopy (checknum,checkname,scannum,visit,checktime,conclusion,other)" +
                    "VALUES('" + gastroscopy.Checknum + "','" + g + "','" + a + "','" + b + "','" + d + "','" + gastroscopy.Conclusion + "','" + f +  "')";
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/" + e, "Visits");
            }

            return View(gastroscopy);
        }


        // GET: Gastroscopies/Create
        public ActionResult Createnianmo(int? id, string scannum, string visit, int refid)
        {
            Gastroscopy gastroscopy = new Gastroscopy();
            gastroscopy.PastId = refid;
            gastroscopy.scannum = scannum;
            gastroscopy.Visit = visit;
            return View(gastroscopy);
        }

        // POST: Gastroscopies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Createnianmo(Gastroscopy gastroscopy)
        {


            if (ModelState.IsValid)
            {
                string a = Request.Form["scannum"];
                string b = Request.Form["Visit"];

                string d = Request.Form["dt"];
                string e = Request.Form["PastId"];

                string g = "胃粘膜血清检查";
                

                string check1= Request.Form["check1"];
                string result1 = Request.Form["result1"];
                string dt1 = Request.Form["dt1"];

                string check2 = Request.Form["check2"];
                string result2 = Request.Form["result2"];
                string dt2 = Request.Form["dt2"];

                string check3 = Request.Form["check3"];
                string result3 = Request.Form["result3"];
                string dt3 = Request.Form["dt3"];

                string check4 = Request.Form["check4"];
                string result4 = Request.Form["result4"];
                string dt4 = Request.Form["dt4"];

                string check5 = Request.Form["check5"];
                string result5 = Request.Form["result5"];
                string dt5 = Request.Form["dt5"];

                MySqlConnection mysql = getMySqlConnection();
                string sql = "";

                int num = Checkweinianmo(a,b);
                num = num + 1;
                sql = " INSERT INTO gastroscopy (checknum,checkname,scannum,visit,checktime,weinianmonum)" +
                    "VALUES('" + gastroscopy.Checknum + "','" + g + "','" + a + "','" + b + "','"+d+"'," + num + ")";

                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();

                int gasid = Getgasid(a,b,num);
                if (check1=="")
                {

                }
                else
                {
                    Insertweinianmo(gasid, check1, result1, dt1);
                }

                if (check2 == "")
                {

                }
                else
                {
                    Insertweinianmo(gasid, check2, result2, dt2);
                }

                if (check3 == "")
                {

                }
                else
                {
                    Insertweinianmo(gasid, check3, result3, dt3);
                }

                if (check4 == "")
                {

                }
                else
                {
                    Insertweinianmo(gasid, check4, result4, dt4);
                }

                if (check5 == "")
                {

                }
                else
                {
                    Insertweinianmo(gasid, check5, result5, dt5);
                }

                return RedirectToAction("Details/" + e, "Visits");
            }

            return View(gastroscopy);
        }


        // GET: Gastroscopies/Edit/5
        public ActionResult Edit(int? id,int detailid,int rawid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gastroscopy gastroscopy = new Gastroscopy();
            gastroscopy.PastId = rawid;
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from gastroscopy where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        gastroscopy.Id = reader.GetInt32("id");
                        gastroscopy.scannum = reader.GetString("scannum");
                        gastroscopy.Visit = reader.GetString("visit");
                        gastroscopy.Checkname = reader.GetString("checkname");
                        gastroscopy.dt = reader.GetDateTime("checktime");
                        gastroscopy.Checknum = reader.GetString("checknum");
                        gastroscopy.Conclusion = reader.GetString("conclusion");
                        gastroscopy.Pathologynum = reader.GetString("pathologynum");
                        gastroscopy.Pathologyconclusion = reader.GetString("pathologyconclusion");
                        gastroscopy.Other = reader.GetString("other");
                        gastroscopy.Pic1 = reader.GetString("pic1");
                        gastroscopy.Pic2 = reader.GetString("pic2");
                        gastroscopy.Pic3 = reader.GetString("pic3");
                        gastroscopy.Pic4 = reader.GetString("pic4");
                        gastroscopy.Pic5 = reader.GetString("pic5");
                        gastroscopy.Pic6 = reader.GetString("pic6");
                        gastroscopy.Pic7 = reader.GetString("pic7");
                        gastroscopy.Pic8 = reader.GetString("pic8");

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

            
            return View(gastroscopy);
        }

        // POST: Gastroscopies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Checktime,Checknum,Conclusion,Pathologynum,Pathologyconclusion,Part")] Gastroscopy gastroscopy)
        {
            string result = "";
            string result1 = "";
            string result2 = "";
            string result3 = "";
            string result4 = "";
            string result5 = "";
            string result6 = "";
            string result7 = "";
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
                gastroscopy.Pic1 = result;
            }

            if (result1 != "error")
            {
                gastroscopy.Pic2 = result1;
            }
            if (result2 != "error")
            {
                gastroscopy.Pic3 = result2;
            }
            if (result3 != "error")
            {
                gastroscopy.Pic4 = result3;
            }
            if (result4 != "error")
            {
                gastroscopy.Pic5 = result4;
            }
            if (result5 != "error")
            {
                gastroscopy.Pic6 = result5;
            }
            if (result6 != "error")
            {
                gastroscopy.Pic7 = result6;
            }
            if (result7 != "error")
            {
                gastroscopy.Pic8 = result7;
            }




            if (ModelState.IsValid)
            {
                string a = Request.Form["scannum"];
                string b = Request.Form["Visit"];
                string g = Request.Form["Other"];
                string d = Request.Form["dt"];
                string e = Request.Form["PastId"];
                string pic1= Request.Form["Pic1"];
                string pic2 = Request.Form["Pic2"];
                string pic3 = Request.Form["Pic3"];
                string pic4 = Request.Form["Pic4"];
                string pic5 = Request.Form["Pic5"];
                string pic6 = Request.Form["Pic6"];
                string pic7 = Request.Form["Pic7"];
                string pic8 = Request.Form["Pic8"];
                if (gastroscopy.Pic1 == null)
                {
                    gastroscopy.Pic1 = pic1;
                }
                if (gastroscopy.Pic2 == null)
                {
                    gastroscopy.Pic2 = pic2;
                }
                if (gastroscopy.Pic3 == null)
                {
                    gastroscopy.Pic3 = pic3;
                }
                if (gastroscopy.Pic4 == null)
                {
                    gastroscopy.Pic4 = pic4;
                }
                if (gastroscopy.Pic5 == null)
                {
                    gastroscopy.Pic5 = pic5;
                }
                if (gastroscopy.Pic6 == null)
                {
                    gastroscopy.Pic6 = pic6;
                }
                if (gastroscopy.Pic7 == null)
                {
                    gastroscopy.Pic7 = pic7;
                }
                if (gastroscopy.Pic8 == null)
                {
                    gastroscopy.Pic8 = pic8;
                }



                MySqlConnection mysql = getMySqlConnection();
                string sql = "";

                sql = "UPDATE  gastroscopy set checktime='"+d+"',checknum='"+gastroscopy.Checknum+ "',conclusion='"+gastroscopy.Conclusion+ "',pathologyconclusion='"+gastroscopy.Pathologyconclusion+
                    "',pathologynum='" +gastroscopy.Pathologynum+ "',other='"+gastroscopy.Other+ "',pic1='"+gastroscopy.Pic1+"',pic2='"+gastroscopy.Pic2+ 
                    "',pic3='" + gastroscopy.Pic3 + "',pic4='" + gastroscopy.Pic4 + "',pic5='" + gastroscopy.Pic5 +
                    "',pic6='" + gastroscopy.Pic6 + "',pic7='" + gastroscopy.Pic7 + "',pic8='" + gastroscopy.Pic8+
                    "'where id=" +gastroscopy.Id;

               
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/" + e, "Visits");
            }

            return View(gastroscopy);
        }


        // GET: Gastroscopies/EditOther/5
        public ActionResult EditOther(int? id, int detailid, int rawid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gastroscopy gastroscopy = new Gastroscopy();
            gastroscopy.PastId = rawid;
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from gastroscopy where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        gastroscopy.Id = reader.GetInt32("id");
                        gastroscopy.scannum = reader.GetString("scannum");
                        gastroscopy.Visit = reader.GetString("visit");
                        gastroscopy.Checkname = reader.GetString("checkname");
                        gastroscopy.dt = reader.GetDateTime("checktime");
                        gastroscopy.Checknum = reader.GetString("checknum");
                        gastroscopy.Conclusion = reader.GetString("conclusion");
                        gastroscopy.Pathologynum = reader.GetString("pathologynum");
                        gastroscopy.Pathologyconclusion = reader.GetString("pathologyconclusion");
                        gastroscopy.Other = reader.GetString("other");
                        gastroscopy.Pic1 = reader.GetString("pic1");
                        gastroscopy.Pic2 = reader.GetString("pic2");
                        gastroscopy.Pic3 = reader.GetString("pic3");
                        gastroscopy.Pic4 = reader.GetString("pic4");
                        gastroscopy.Pic5 = reader.GetString("pic5");

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


            return View(gastroscopy);
        }

        // POST: Gastroscopies/EditOther/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOther([Bind(Include = "Id,Checktime,Checknum,Conclusion,Pathologynum,Pathologyconclusion,Part")] Gastroscopy gastroscopy)
        {
            string result = "";
            string result1 = "";
            string result2 = "";
            string result3 = "";
            string result4 = "";
            HttpPostedFileBase files = Request.Files["filename"];
            HttpPostedFileBase files1 = Request.Files["filename1"];
            HttpPostedFileBase files2 = Request.Files["filename2"];
            HttpPostedFileBase files3 = Request.Files["filename3"];
            HttpPostedFileBase files4 = Request.Files["filename4"];

            result = SaveImage(files);
            result1 = SaveImage(files1);
            result2 = SaveImage(files2);
            result3 = SaveImage(files3);
            result4 = SaveImage(files4);

            if (result !="error")
            {
                gastroscopy.Pic1 = result;
            }

            if (result1 != "error")
            {
                gastroscopy.Pic2 = result1;
            }
            if (result2 != "error")
            {
                gastroscopy.Pic3 = result2;
            }
            if (result3 != "error")
            {
                gastroscopy.Pic4 = result3;
            }
            if (result4 != "error")
            {
                gastroscopy.Pic2 = result4;
            }



            if (ModelState.IsValid)
            {
                string a = Request.Form["scannum"];
                string b = Request.Form["Visit"];
                string g = Request.Form["Checkname"];
                string d = Request.Form["dt"];
                string e = Request.Form["PastId"];
                string pic1 = Request.Form["Pic1"];
                string pic2 = Request.Form["Pic2"];
                string pic3 = Request.Form["Pic3"];
                string pic4 = Request.Form["Pic4"];
                string pic5 = Request.Form["Pic5"];
                if (gastroscopy.Pic1 == null)
                {
                    gastroscopy.Pic1 = pic1;
                }
                if (gastroscopy.Pic2 == null)
                {
                    gastroscopy.Pic2 = pic2;
                }
                if (gastroscopy.Pic3 == null)
                {
                    gastroscopy.Pic3 = pic3;
                }
                if (gastroscopy.Pic4 == null)
                {
                    gastroscopy.Pic4 = pic4;
                }
                if (gastroscopy.Pic5 == null)
                {
                    gastroscopy.Pic5 = pic5;
                }



                MySqlConnection mysql = getMySqlConnection();
                string sql = "";

                sql = "UPDATE  gastroscopy set checktime='" + d + "',checknum='" + gastroscopy.Checknum + "',conclusion='" + gastroscopy.Conclusion +  
                    "',checkname='" + g +  "',pic1='" + gastroscopy.Pic1 + "',pic2='" + gastroscopy.Pic2 +
                    "',pic3='" + gastroscopy.Pic3 + "',pic4='" + gastroscopy.Pic4 + "',pic5='" + gastroscopy.Pic5 + "'where id=" + gastroscopy.Id;


                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/" + e, "Visits");
            }

            return View(gastroscopy);
        }


        // GET: Gastroscopies/Edityoumen/5
        public ActionResult Edityoumen(int? id, int detailid, int rawid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gastroscopy gastroscopy = new Gastroscopy();
            gastroscopy.PastId = rawid;
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from gastroscopy where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        gastroscopy.Id = reader.GetInt32("id");
                        gastroscopy.Checknum = reader.GetString("checknum");
                        gastroscopy.scannum = reader.GetString("scannum");
                        gastroscopy.Visit = reader.GetString("visit");
                        gastroscopy.Checkname = reader.GetString("checkname");
                        gastroscopy.dt = reader.GetDateTime("checktime");
                        
                        gastroscopy.Conclusion = reader.GetString("conclusion");
                        
                        gastroscopy.Other = reader.GetString("other");
                      

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


            return View(gastroscopy);
        }

        // POST: Gastroscopies/Edityoumen/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edityoumen( Gastroscopy gastroscopy)
        {
           


            if (ModelState.IsValid)
            {
                string a = Request.Form["scannum"];
                string b = Request.Form["Visit"];
                string g = Request.Form["isInfected"];
                string d = Request.Form["dt"];
                string e = Request.Form["PastId"];
               
              

                MySqlConnection mysql = getMySqlConnection();
                string sql = "";

                sql = "UPDATE  gastroscopy set checktime='" + d  + "',conclusion='" + gastroscopy.Conclusion +
                    "',checknum='" + gastroscopy.Checknum +
                    "',other='" + g  + "'where id=" + gastroscopy.Id;


                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/" + e, "Visits");
            }

            return View(gastroscopy);
        }


        // GET: Gastroscopies/Editweinianmo/5
        public ActionResult Editweinianmo(int? id, int detailid, int rawid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gastroscopy gastroscopy = new Gastroscopy();
            gastroscopy.PastId = rawid;
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from gastroscopy where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        gastroscopy.Id = reader.GetInt32("id");
                        gastroscopy.scannum = reader.GetString("scannum");
                        gastroscopy.Checknum = reader.GetString("checknum");
                        gastroscopy.Visit = reader.GetString("visit");
                        gastroscopy.Checkname = reader.GetString("checkname");
                        gastroscopy.dt = reader.GetDateTime("checktime");

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

            gastroscopy.getweinianmo = Getweinianmo(Convert.ToInt32(id));
            return View(gastroscopy);
        }

        // POST: Gastroscopies/Editweinianmo/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editweinianmo(Gastroscopy gastroscopy)
        {



            if (ModelState.IsValid)
            {
                string a = Request.Form["scannum"];
                string b = Request.Form["Visit"];

                string d = Request.Form["dt"];
                string e = Request.Form["PastId"];

                string g = "胃粘膜血清检查";

                string id1 = Request.Form["id1"];
                string check1 = Request.Form["check1"];
                string result1 = Request.Form["result1"];
                string dt1 = Request.Form["dt1"];

                string id2 = Request.Form["id2"];
                string check2 = Request.Form["check2"];
                string result2 = Request.Form["result2"];
                string dt2 = Request.Form["dt2"];

                string id3 = Request.Form["id3"];
                string check3 = Request.Form["check3"];
                string result3 = Request.Form["result3"];
                string dt3 = Request.Form["dt3"];

                string id4 = Request.Form["id4"];
                string check4 = Request.Form["check4"];
                string result4 = Request.Form["result4"];
                string dt4 = Request.Form["dt4"];

                string id5 = Request.Form["id5"];
                string check5 = Request.Form["check5"];
                string result5 = Request.Form["result5"];
                string dt5 = Request.Form["dt5"];



                MySqlConnection mysql = getMySqlConnection();
                string sql = "";

                sql = "UPDATE  gastroscopy set checktime='" + d +
                    "',checknum='" + gastroscopy.Checknum +
                     "' where id=" + gastroscopy.Id;
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();


                Updateweinianmo(Convert.ToInt32(id1), check1, result1, dt1);

                if (check2 == ""&& id2 =="0")
                {

                }
                else if (check2 != "" && id2 == "0")
                {
                    Insertweinianmo(gastroscopy.Id, check2, result2, dt2);
                }
                else if(check2 != "" && id2 != "0")
                {
                    Updateweinianmo(Convert.ToInt32(id2), check2, result2, dt2);
                    
                }

                if (check3 == "" && id3 == "0")
                {

                }
                else if (check3 != "" && id3 == "0")
                {
                    Insertweinianmo(gastroscopy.Id, check3, result3, dt3);
                }
                else if (check3 != "" && id3 != "0")
                {
                    Updateweinianmo(Convert.ToInt32(id3), check3, result3, dt3);
                    
                }


                if (check4 == "" && id4 == "0")
                {

                }
                else if (check4 != "" && id4 == "0")
                {
                    Insertweinianmo(gastroscopy.Id, check4, result4, dt4);
                }
                else if (check4 != "" && id4 != "0")
                {
                    Updateweinianmo(Convert.ToInt32(id4), check4, result4, dt4);

                }

                if (check5 == "" && id5 == "0")
                {

                }
                else if (check5 != "" && id5 == "0")
                {
                    Insertweinianmo(gastroscopy.Id, check5, result5, dt5);
                }
                else if (check5 != "" && id5 != "0")
                {
                    Updateweinianmo(Convert.ToInt32(id5), check5, result5, dt5);

                }

                return RedirectToAction("Details/" + e, "Visits");
            }

            return View(gastroscopy);
        }


        // GET: Gastroscopies/Delete/5
        public ActionResult Delete(int? id,int refid)
        {
           
            Gastroscopy gastroscopy = new Gastroscopy();
            gastroscopy.PastId = refid;
            return View(gastroscopy);
        }

        // POST: Gastroscopies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string a = Request.Form["PastId"];
            int b = Convert.ToInt32(a);

            string sql = "";
            sql = "SET FOREIGN_KEY_CHECKS=0;DELETE FROM gastroscopy where id=" + id;
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();

            return RedirectToAction("Details/" + b, "Visits");
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

        //插入胃粘膜血清
        private void Insertweinianmo(int gasid, string check, string result, string checktime)
        {

            MySqlConnection mysql = getMySqlConnection();
            string sql = "INSERT INTO gasweinianmo(gasid,checks,result,checktime)VALUES" + "(" +
                gasid + ",'" + check + "','" + result + "'," + "'" + checktime  +  " ')";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();

        }
        //得到胃粘膜血清一次就诊中最高的次数
        private int Checkweinianmo(string scannum, string visit)
        {
            int a = 0;
            int i = 0;
            int[] b = new int[10];
            MySqlConnection mysql = getMySqlConnection();
            string sql = "select weinianmonum from gastroscopy where checkname= '胃粘膜血清检查' and scannum= '" +
                scannum + "' and visit= '" + visit + "'" ;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        
                        int c;
                        c= reader.GetInt32("weinianmonum");
                        b[i] = c;
                    }
                    i++;
                }


            }
            catch
            {
                
            }
            finally
            {
                mysql.Close();
            }
            int max = b[0];
            for (int j = 0; j < b.Length; j++)
            {
                if (max < b[j])
                {
                    max = b[j];
                }
            }
            if (max>0)
            {
                a = max;
            }
            return a;
        }
        //得到就诊表胃粘膜血清检查的id
        private int Getgasid(string scannum, string visit ,int weinianmonum)
        {
            int a = 0;
            
            MySqlConnection mysql = getMySqlConnection();
            string sql = "select id from gastroscopy where checkname= '胃粘膜血清检查' and scannum= '" +
                scannum + "' and visit= '" + visit + "' and weinianmonum="+ weinianmonum;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                       
                        a = reader.GetInt32("id");
                        
                    }
                    
                }


            }
            catch
            {

            }
            finally
            {
                mysql.Close();
            }
           
            return a;
        }

        private DataTable Getweinianmo(int gasid)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from gasweinianmo where gasid=" + gasid ;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        //跟新胃粘膜血清
        private void Updateweinianmo(int id, string check, string result, string checktime)
        {

            MySqlConnection mysql = getMySqlConnection();
            string sql = "UPDATE gasweinianmo set checks='" + check + "',result='" + result + "',checktime="
                + "'" + checktime + "'"  + " where id=" + id;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();

        }

    }
}
