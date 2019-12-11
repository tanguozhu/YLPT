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
    public class AdversereactionsController : Controller
    {
        private AdversereactionsDbContext db = new AdversereactionsDbContext();

        // GET: Adversereactions
        public ActionResult Index()
        {
            return View(db.Adversereactions.ToList());
        }

        // GET: Adversereactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adversereactions adversereactions = db.Adversereactions.Find(id);
            if (adversereactions == null)
            {
                return HttpNotFound();
            }
            return View(adversereactions);
        }

        // GET: Adversereactions/Create
        public ActionResult Create(int? id, int treatmentid)
        {
            Adversereactions adversereactions = new Adversereactions();
            adversereactions.treatmentid = treatmentid;
            adversereactions.Id = 0;
            DataTable dt = new DataTable();
            adversereactions.type0 = dt;
            adversereactions.type1 = dt;
            adversereactions.type2 = dt;
            adversereactions.type3 = dt;
            adversereactions.type4 = dt;
            adversereactions.type5 = dt;
            adversereactions.type6 = dt;
            return View(adversereactions);
        }

        // POST: Adversereactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Adversereactions adversereactions)
        {
            string refid = Request.Form["treatmentid"];
            if (ModelState.IsValid)
            {
                string[,] getall = new string[7, 10];
                for (int i = 0; i < 7; i++)
                {
                    getall[i, 0] = Request.Form["id" + i];
                    getall[i, 2] = Request.Form["beginTime" + i];
                    getall[i, 3] = Request.Form["lastTime" + i];
                    getall[i, 4] = Request.Form["degree" + i];
                    getall[i, 5] = Request.Form["frequency" + i];
                    getall[i, 6] = Request.Form["isNeedTreatment" + i];
                    getall[i, 7] = Request.Form["effectToMed" + i];
                    getall[i, 8] = Request.Form["relationshipWithMed" + i];
                    getall[i, 9] = Request.Form["ending" + i];
                }
                for (int i = 0; i < 7; i++)
                {

                    InsertAdverseractions(Convert.ToInt32(refid), i, Convert.ToDateTime(getall[i, 2]), getall[i, 3], getall[i, 4],
                       getall[i, 5], Convert.ToInt32(getall[i, 6]), getall[i, 7], getall[i, 8], getall[i, 9]);

                }
                return RedirectToAction("TreatmentDetails/" + Convert.ToInt32(refid), "Visits");
            }

            return View(adversereactions);
        }

        // GET: Adversereactions/Edit/5
        public ActionResult Edit(int? id,int treatmentid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adversereactions adversereactions = new Adversereactions();
            adversereactions.Id = 0;
            adversereactions.treatmentid = treatmentid;
            adversereactions.type0 = GetAdversereactions(treatmentid, 0);
            adversereactions.type1 = GetAdversereactions(treatmentid, 1);
            adversereactions.type2 = GetAdversereactions(treatmentid, 2);
            adversereactions.type3 = GetAdversereactions(treatmentid, 3);
            adversereactions.type4 = GetAdversereactions(treatmentid, 4);
            adversereactions.type5 = GetAdversereactions(treatmentid, 5);
            adversereactions.type6 = GetAdversereactions(treatmentid, 6);
            return View(adversereactions);
        }

        // POST: Adversereactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Adversereactions adversereactions)
        {
            string refid = Request.Form["treatmentid"];
            if (ModelState.IsValid)
            {
                string[,] getall=new string[7,10];
                for (int i = 0; i < 7; i++)
                {
                    getall[i, 0] = Request.Form["id" + i];
                    getall[i, 2] = Request.Form["beginTime" + i];
                    getall[i, 3] = Request.Form["lastTime" + i];
                    getall[i, 4] = Request.Form["degree" + i];
                    getall[i, 5] = Request.Form["frequency" + i];
                    getall[i, 6] = Request.Form["isNeedTreatment" + i];
                    getall[i, 7] = Request.Form["effectToMed" + i];
                    getall[i, 8] = Request.Form["relationshipWithMed" + i];
                    getall[i, 9] = Request.Form["ending" + i];
                }
                for (int i = 0; i < 7; i++)
                {

                    UpdateAdverseractions(Convert.ToInt32(getall[i,0]),i,Convert.ToDateTime(getall[i, 2]), getall[i, 3], getall[i, 4],
                       getall[i, 5],Convert.ToInt32(getall[i, 6]), getall[i, 7], getall[i, 8], getall[i, 9]);

                }



                return RedirectToAction("TreatmentDetails/" + Convert.ToInt32(refid), "Visits");
            }
            return View(adversereactions);
        }

        // GET: Adversereactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adversereactions adversereactions = db.Adversereactions.Find(id);
            if (adversereactions == null)
            {
                return HttpNotFound();
            }
            return View(adversereactions);
        }

        // POST: Adversereactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adversereactions adversereactions = db.Adversereactions.Find(id);
            db.Adversereactions.Remove(adversereactions);
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

        private DataTable GetAdversereactions(int treatmentid,int type)
        {
            DataTable dt = new DataTable();
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from adversereactions  where treatmentid=" + treatmentid+" and type="+type;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            
            command.Fill(dt);
            return dt;
        }

        public static void UpdateAdverseractions(int id,  int type, DateTime beginTime, string lastTime,
            string degree, string frequency, int isNeedTreatment, string effectToMed, string relationshipWithMed, string ending)
        {
            
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = " UPDATE adversereactions set type=" + type + ",beginTime='" + beginTime + "',lastTime='" + lastTime + "',degree='" +
                degree + "',frequency='" + frequency + "',isNeedTreatment=" + isNeedTreatment + ",effectToMed='" + effectToMed + "',relationshipWithMed='" +
                relationshipWithMed + "',ending='" + ending + "'"  + " where id=" + id;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();

           
            
        }

        public static void InsertAdverseractions( int treatmentid, int type, DateTime beginTime, string lastTime,
           string degree, string frequency, int isNeedTreatment, string effectToMed, string relationshipWithMed, string ending)
        {

            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = " INSERT INTO adversereactions (treatmentid,type,beginTime,lastTime,degree,frequency,isNeedTreatment,effectToMed," +
                "relationshipWithMed,ending)VALUES(" + treatmentid + "," + type + ",'" + beginTime + "','"+ lastTime+"','"  + degree + "','"+
                frequency + "'," + isNeedTreatment + ",'" + effectToMed + "','" + relationshipWithMed + "','" + ending + "' )" ;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();



        }


    }
}
