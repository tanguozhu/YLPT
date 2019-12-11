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
            string sql = "SELECT USER.NAME,visits.id,visits.visitId,visits.date,visits.scannum,visits.identinum,visits.returnvisit," +
                "visits.returnvisitcontent,visits.returnvisitdate FROM USER,visits WHERE USER.scannum = visits.scannum AND" +
                " visits.returnvisit=1 AND" +
                " visits.returnvisitdate = '" +d+"'";
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
                        returnVisit.Id= reader.GetInt32("id");
                        returnVisit.Name = reader.GetString("name");

                        returnVisit.VisitId = reader.GetString("visitId");
                        DateTime dt = new DateTime();
                        dt = reader.GetDateTime("date");
                        returnVisit.Date = dt.ToString("yyyy-MM-dd");
                        returnVisit.returnvisit = reader.GetInt32("returnvisit");
                        returnVisit.scannum = reader.GetString("scannum");
                        returnVisit.Identinum = reader.GetString("identinum");
                        returnVisit.returnvisitcontent = reader.GetString("returnvisitcontent");
                        DateTime dt1 = new DateTime();
                        dt1 = reader.GetDateTime("returnvisitdate");
                        returnVisit.returnvisitdate= dt1.ToString("yyyy-MM-dd");


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
            string sql = "SELECT USER.NAME,visits.id,visits.visitId,visits.date,visits.scannum,visits.identinum,visits.returnvisit," +
               "visits.returnvisitcontent,visits.returnvisitdate FROM USER,visits WHERE USER.scannum = visits.scannum AND" +
               " visits.returnvisit=1 AND" +
               " visits.returnvisitdate = '" + d + "'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        returnVisit.Id = reader.GetInt32("id");
                        returnVisit.Name = reader.GetString("name");

                        returnVisit.VisitId = reader.GetString("visitId");
                        DateTime dt = new DateTime();
                        dt = reader.GetDateTime("date");
                        returnVisit.Date = dt.ToString("yyyy-MM-dd");
                        returnVisit.returnvisit = reader.GetInt32("returnvisit");
                        returnVisit.scannum = reader.GetString("scannum");
                        returnVisit.Identinum = reader.GetString("identinum");
                        returnVisit.returnvisitcontent = reader.GetString("returnvisitcontent");
                        DateTime dt1 = new DateTime();
                        dt1 = reader.GetDateTime("returnvisitdate");
                        returnVisit.returnvisitdate = dt1.ToString("yyyy-MM-dd");

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
