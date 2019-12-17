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
            if (ModelState.IsValid)
            {
                string a = Request.Form["refid"];
                string b = Request.Form["visit"];
                string c = Request.Form["scannum"];
                MySqlConnection mysql = getMySqlConnection();
                string sql = "";
                sql = "INSERT INTO followup (visit,scannum,date,content)VALUES('" + b+"','" + c + "','" +
                    followUp.date + "'," + "'" + followUp.content + "')";
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

		public int getcheck_id(string visit, string scannum, int followupnum)
		{
			string sql = "select id from gastroscopy where visit='"+visit+ "' and scannum='"+scannum+"' followupnum="+followupnum+"";
			MySqlConnection mysql = getMySqlConnection();
			MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
			mysql.Open();
			MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
			mySqlCommand.ExecuteNonQuery();
			mysql.Close();
			DataTable dt = new DataTable();
			command.Fill(dt);
			int check_id =(int)dt.Rows[0][0];
			return check_id;

		}
		public string getcheck_num(string visit, string scannum, int followupnum)
		{
			string sql = "select checknum from gastroscopy where visit='" + visit + "' and scannum='" + scannum + "' followupnum=" + followupnum + "";
			MySqlConnection mysql = getMySqlConnection();
			MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
			mysql.Open();
			MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
			mySqlCommand.ExecuteNonQuery();
			mysql.Close();
			DataTable dt = new DataTable();
			command.Fill(dt);
			string check_num =(string) dt.Rows[0][0];
			return check_num;
		}
		public int gettreat_id(string visit, string scannum, int followupnum)
		{
			string sql = "select id from treatment where visit='" + visit + "' and scannum='" + scannum + "' followupnum=" + followupnum + "";
			MySqlConnection mysql = getMySqlConnection();
			MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
			mysql.Open();
			MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
			mySqlCommand.ExecuteNonQuery();
			mysql.Close();
			DataTable dt = new DataTable();
			command.Fill(dt);
			int treat_id = (int)dt.Rows[0][0];
			return treat_id;
		}

	}
}
