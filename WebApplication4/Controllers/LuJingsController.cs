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
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class LuJingsController : Controller
    {
        private LuJingDbContext db = new LuJingDbContext();

        // GET: LuJings
        public ActionResult Index()
        {
            
            LuJing luJing = new LuJing();
           
            return View();
        }

        // GET: LuJings/Details/5
        public ActionResult Details(int page = 1)
        {
            List<LuJing> listlujing = new List<LuJing>();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from medical_schemes", mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            var i = 0;
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        LuJing luJing = new LuJing();
                        luJing.Id = reader.GetInt32("id");

                        luJing.scheme = reader.GetString("scheme");
                        luJing.rs = reader.GetString("rs");
                        luJing.rs_num = reader.GetString("rs_num");
                        luJing.hospital_time = reader.GetInt32("hospital_time");
                        luJing.cost_all = reader.GetInt32("cost_all");
                        luJing.cost_town = reader.GetInt32("cost_town");
                        luJing.cost_country = reader.GetInt32("cost_country");

                        listlujing.Add(luJing);
                        //db.WebMangers.Add(webmanger);
                        i = i + 1;
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
            var data = listlujing
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);


            return View(data);
        }

        // GET: LuJings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LuJings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,pic")] LuJing luJing)
        {
            string shengao = Request.Form["shengao"];
            string tizhong = Request.Form["tizhong"];
            string tibaiomianji = Request.Form["tibaiomianji"];
            string fenqi = Request.Form["fenqi"];
            string nianling = Request.Form["nianling"];
            string leixing = Request.Form["leixing"];
            string sql = "";
            
                MySqlConnection mysql = getMySqlConnection();
                sql = " INSERT INTO medical_patient (stage,age,insurance,height,weight,area)" +
                   "VALUES(" + Convert.ToInt32(fenqi) + "," + Convert.ToInt32(nianling) + "," + Convert.ToInt32(leixing) + 
                   "," + Convert.ToInt32(shengao) + "," + Convert.ToInt32(tizhong) + ",'" +
                   tibaiomianji + "')";
                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Index" , "Students");
                
           
        }

        // GET: LuJings/Edit/5
        public ActionResult Edit(int page = 1)
        {
            List<LuJing> listlujing = new List<LuJing>();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from medical_schemes", mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            var i = 0;
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        LuJing luJing = new LuJing();
                        luJing.Id = reader.GetInt32("id");

                        luJing.scheme = reader.GetString("scheme");
                        luJing.rs = reader.GetString("rs");
                        luJing.rs_num = reader.GetString("rs_num");
                        luJing.hospital_time = reader.GetInt32("hospital_time");
                        luJing.cost_all = reader.GetInt32("cost_all");
                        luJing.cost_town = reader.GetInt32("cost_town");
                        luJing.cost_country = reader.GetInt32("cost_country");

                        listlujing.Add(luJing);
                        //db.WebMangers.Add(webmanger);
                        i = i + 1;
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
            var data = listlujing
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);


            return View(data);
        }

        // POST: LuJings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,pic")] LuJing luJing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(luJing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(luJing);
        }

        // GET: LuJings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LuJing luJing = db.LuJings.Find(id);
            if (luJing == null)
            {
                return HttpNotFound();
            }
            return View(luJing);
        }

        // POST: LuJings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LuJing luJing = db.LuJings.Find(id);
            db.LuJings.Remove(luJing);
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
