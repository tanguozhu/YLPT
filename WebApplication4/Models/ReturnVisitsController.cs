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

namespace WebApplication4.Models
{
    public class ReturnVisitsController : Controller
    {
        private ReturnVisitDbContext db = new ReturnVisitDbContext();

        // GET: ReturnVisits
        public ActionResult Index(int page = 1)
        {
            string d = DateTime.Now.ToString("yyyy-MM-dd");
            List<ReturnVisit> listreturnvisits = new List<ReturnVisit>();
            MySqlConnection mysql = getMySqlConnection();
            string sql = "SELECT `user` .`name`,`user`.phone,`user`.identinum,followup.visit,followup.scannum,followup.weijing_check," +
        "followup.weijing_check_time,followup.weinianmo_check,followup.weinianmo_check_time,followup.youmen_check," +
        "followup.youmen_check_time,followup.other_check,followup.other_check_time,followup.youmen_treat," +
        "followup.youmen_treat_time,followup.operater_treat,followup.operater_treat_time,followup.other_treat," +
        "followup.other_treat_time FROM `user`,followup WHERE`user`.scannum = followup.scannum AND(weijing_check_time ='" +
        d+"'OR weinianmo_check_time = '"+ d +"'OR youmen_check_time = '"+ d +"'OR other_check_time = '"+d+"'OR youmen_treat_time ='"+
        d+"'OR operater_treat_time = '"+d+"'OR other_treat_time = '"+d+"');";
            MySqlCommand mySqlCommand = getSqlCommand(sql,mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        ReturnVisit returnVisit = new ReturnVisit();
                        
                        returnVisit.Name = reader.GetString("name");
                        
                        returnVisit.VisitId = reader.GetString("visit");
                        returnVisit.scannum= reader.GetString("scannum");
                        returnVisit.Identinum = reader.GetString("identinum");

                        returnVisit.weijing_check = reader.GetInt32("weijing_check");
                        returnVisit.weijing_check_time = reader.GetDateTime("weijing_check_time");

                        returnVisit.weinianmo_check = reader.GetInt32("weinianmo_check");
                        returnVisit.weinianmo_check_time = reader.GetDateTime("weinianmo_check_time");

                        returnVisit.youmen_check = reader.GetInt32("youmen_check");
                        returnVisit.youmen_check_time = reader.GetDateTime("youmen_check_time");

                        returnVisit.other_check = reader.GetInt32("other_check");
                        returnVisit.other_check_time = reader.GetDateTime("other_check_time");

                        returnVisit.operater_treat = reader.GetInt32("operater_treat");
                        returnVisit.operater_treat_time = reader.GetDateTime("operater_treat_time");

                        returnVisit.youmen_treat = reader.GetInt32("youmen_treat");
                        returnVisit.youmen_treat_time = reader.GetDateTime("youmen_treat_time");

                        returnVisit.other_treat = reader.GetInt32("other_treat");
                        returnVisit.other_treat_time = reader.GetDateTime("other_treat_time");

                        listreturnvisits.Add(returnVisit);

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
            var data = listreturnvisits
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);
            return View(data);
        }

        // GET: ReturnVisits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnVisit returnVisit = new ReturnVisit();
            MySqlConnection mysql = getMySqlConnection();
            string d = DateTime.Now.ToString("yyyy-MM-dd");
            string sql = "SELECT `user` .`name`,`user`.phone,`user`.identinum,followup.visit,followup.scannum,followup.weijing_check," +
        "followup.weijing_check_time,followup.weinianmo_check,followup.weinianmo_check_time,followup.youmen_check," +
        "followup.youmen_check_time,followup.other_check,followup.other_check_time,followup.youmen_treat," +
        "followup.youmen_treat_time,followup.operater_treat,followup.operater_treat_time,followup.other_treat," +
        "followup.other_treat_time FROM `user`,followup WHERE`user`.scannum = followup.scannum";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        returnVisit.Name = reader.GetString("name");

                        returnVisit.VisitId = reader.GetString("visit");
                        returnVisit.scannum = reader.GetString("scannum");
                        returnVisit.Identinum = reader.GetString("identinum");

                        returnVisit.weijing_check = reader.GetInt32("weijing_check");
                        returnVisit.weijing_check_time = reader.GetDateTime("weijing_check_time");

                        returnVisit.weinianmo_check = reader.GetInt32("weinianmo_check");
                        returnVisit.weinianmo_check_time = reader.GetDateTime("weinianmo_check_time");

                        returnVisit.youmen_check = reader.GetInt32("youmen_check");
                        returnVisit.youmen_check_time = reader.GetDateTime("youmen_check_time");

                        returnVisit.other_check = reader.GetInt32("other_check");
                        returnVisit.other_check_time = reader.GetDateTime("other_check_time");

                        returnVisit.operater_treat = reader.GetInt32("operater_treat");
                        returnVisit.operater_treat_time = reader.GetDateTime("operater_treat_time");

                        returnVisit.youmen_treat = reader.GetInt32("youmen_treat");
                        returnVisit.youmen_treat_time = reader.GetDateTime("youmen_treat_time");

                        returnVisit.other_treat = reader.GetInt32("other_treat");
                        returnVisit.other_treat_time = reader.GetDateTime("other_treat_time");

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
            if (returnVisit == null)
            {
                return HttpNotFound();
            }
            
            return View(returnVisit);
        }

        // GET: ReturnVisits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReturnVisits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,scannum,Identinum,VisitId,Date,Name,returnvisit,returnvisitcontent,returnvisitdate")] ReturnVisit returnVisit)
        {
            if (ModelState.IsValid)
            {
                db.returnVisits.Add(returnVisit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(returnVisit);
        }

        // GET: ReturnVisits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnVisit returnVisit = db.returnVisits.Find(id);
            if (returnVisit == null)
            {
                return HttpNotFound();
            }
            return View(returnVisit);
        }

        // POST: ReturnVisits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,scannum,Identinum,VisitId,Date,Name,returnvisit,returnvisitcontent,returnvisitdate")] ReturnVisit returnVisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(returnVisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(returnVisit);
        }

        // GET: ReturnVisits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReturnVisit returnVisit = db.returnVisits.Find(id);
            if (returnVisit == null)
            {
                return HttpNotFound();
            }
            return View(returnVisit);
        }

        // POST: ReturnVisits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReturnVisit returnVisit = db.returnVisits.Find(id);
            db.returnVisits.Remove(returnVisit);
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
    }
}
