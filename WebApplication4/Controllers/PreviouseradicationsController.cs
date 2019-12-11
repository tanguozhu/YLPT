using MySql.Data.MySqlClient;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class PreviouseradicationsController : Controller
    {
        private PreviouseradicationDbContext db = new PreviouseradicationDbContext();

        // GET: Previouseradications
        public ActionResult Index()
        {
            return View(db.Previouseradications.ToList());
        }

        // GET: Previouseradications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Previouseradication previouseradication = new Previouseradication();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from previouseradication where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        
                        previouseradication.Id = reader.GetInt32("id");
                        previouseradication.Phone = reader.GetString("phone");

                        previouseradication.Radicationtime = reader.GetString("radicationtime");
                        previouseradication.Radicationdate = reader.GetDateTime("radicationdate");
                        previouseradication.Radicationcase = reader.GetString("radicationcase");
                        previouseradication.Course = reader.GetString("course");



                        previouseradication.Result = reader.GetString("result");


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
            if (previouseradication == null)
            {
                return HttpNotFound();
            }
            return View(previouseradication);
        }

        // GET: Previouseradications/Create
        public ActionResult Create(int? id, string phone, int refid)
        {
            Previouseradication previouseradication = new Previouseradication();
            previouseradication.Phone = phone;
            previouseradication.RefId = refid;
            return View();
        }

        // POST: Previouseradications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RefId,Id,Radicationtime,Radicationdate,Radicationcase,Course,Result")] Previouseradication previouseradication)
        {
            if (ModelState.IsValid)
            {
                string a = Request.Form["RefId"];
                int b = Convert.ToInt32(a);
                previouseradication.Phone = Request.Form["Phone"];
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("INSERT INTO previouseradication(phone,radicationtime,radicationdate,radicationcase,course,result)VALUES" + "(" +
                "'" + previouseradication.Phone + "','" + previouseradication.Radicationtime + "','" + previouseradication.Radicationdate + "','" + previouseradication.Radicationcase + "','" + previouseradication.Course + "','" +
                previouseradication.Result + "'" + ")", mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/" + b, "UserMangers");
            }

            return View(previouseradication);
        }

        // GET: Previouseradications/Edit/5
        public ActionResult Edit(int? id,int RfId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Previouseradication previouseradication = new Previouseradication();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from previouseradication where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        previouseradication.RefId = RfId;
                        previouseradication.Id = reader.GetInt32("id");
                        previouseradication.Phone = reader.GetString("phone");

                        previouseradication.Radicationtime = reader.GetString("radicationtime");
                        previouseradication.Radicationdate = reader.GetDateTime("radicationdate");
                        previouseradication.Radicationcase = reader.GetString("radicationcase");
                        previouseradication.Course = reader.GetString("course");


                        
                        previouseradication.Result = reader.GetString("result");
                       

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
            if (previouseradication == null)
            {
                return HttpNotFound();
            }
            
            return View(previouseradication);
        }

        // POST: Previouseradications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RefId,Id,Radicationtime,Radicationdate,Radicationcase,Course,Result")] Previouseradication previouseradication)
        {
            if (ModelState.IsValid)
            {
                string a = Request.Form["RefId"];
                int b = Convert.ToInt32(a);
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("UPDATE previouseradication set radicationtime=" +"'"+
                 previouseradication.Radicationtime + "',radicationdate='" + previouseradication.Radicationdate + "',radicationcase='" + previouseradication.Radicationcase + "',course='" +
                 previouseradication.Course + "',result='" + previouseradication.Result +  "'" + "where id=" + previouseradication.Id, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/" + b, "UserMangers");
            }
            return View(previouseradication);
        }

        // GET: Previouseradications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Previouseradication previouseradication = new Previouseradication();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from previouseradication where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        
                        previouseradication.Id = reader.GetInt32("id");
                        previouseradication.Phone = reader.GetString("phone");

                        previouseradication.Radicationtime = reader.GetString("radicationtime");
                        previouseradication.Radicationdate = reader.GetDateTime("radicationdate");
                        previouseradication.Radicationcase = reader.GetString("radicationcase");
                        previouseradication.Course = reader.GetString("course");



                        previouseradication.Result = reader.GetString("result");


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
            if (previouseradication == null)
            {
                return HttpNotFound();
            }
            return View(previouseradication);
        }

        // POST: Previouseradications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("delete from previouseradication where id="+id, mysql);
            mysql.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            return RedirectToAction("Index" , "UserMangers");
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
