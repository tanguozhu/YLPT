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
    
    public class WebMangersController : Controller
    {
        private WebMangerDbContext db = new WebMangerDbContext();

        
        // GET: WebMangers
        public ActionResult Index(int page=1)
        {
            List<WebManger> listwebManger = new List<WebManger>();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from webmanger", mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            var i=0;
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        WebManger webmanger = new WebManger();
                        webmanger.Id = reader.GetInt32("id");

                        webmanger.Account = reader.GetString("account");
                        webmanger.Password = reader.GetString("password");
                        webmanger.Name = reader.GetString("name");
                        webmanger.webkind = reader.GetInt32("webkind");
                        webmanger.modify = reader.GetInt32("modif");
                        webmanger.delete = reader.GetInt32("delet");
                        webmanger.insert = reader.GetInt32("inser");
                        listwebManger.Add(webmanger);
                        //db.WebMangers.Add(webmanger);
                        i=i+1;
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
            const int pagesize = 5;
            var data = listwebManger
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);


            return View(data);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string Account, string Name,int page=1)
        {
            List<WebManger> listwebManger1 = new List<WebManger>();
            MySqlConnection mysql = getMySqlConnection();
            string con;
            if (Account == "" && Name == "")
            {
                con = "select * from webmanger";
            }
            else if (Account != "" && Name != "")
            {
                con = "select * from webmanger where (account " +
                    "like" + "'%" + Account + "%'" + "and name like " + "'%" + Name + "%')";
            }
            else if (Account != "" && Name == "")
            {
                con = "select * from webmanger where account " +
                   "like" + "'%" + Account + "%'";
            }
            else
            {
                con = "select * from webmanger where  " +
                     " Name like " + "'%" + Name + "%'";
            }


            MySqlCommand mySqlCommand = getSqlCommand(con , mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            var i = 0;
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        WebManger webmanger1 = new WebManger();
                        webmanger1.Id = reader.GetInt32("id");

                        webmanger1.Account = reader.GetString("account");
                        webmanger1.Password = reader.GetString("password");
                        webmanger1.Name = reader.GetString("name");
                        webmanger1.webkind = reader.GetInt32("webkind");
                        webmanger1.modify = reader.GetInt32("modif");
                        webmanger1.delete = reader.GetInt32("delet");
                        webmanger1.insert = reader.GetInt32("inser");
                        listwebManger1.Add(webmanger1);
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
            const int pagesize = 5;
            var data = listwebManger1
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);


            return View(data);
            //return View(listwebManger1);
        }
        // GET: WebMangers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebManger webmanger = new WebManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from webmanger where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        webmanger.Id = reader.GetInt32("id");
                        webmanger.Account = reader.GetString("account");
                        
                        webmanger.Password = reader.GetString("password");
                        webmanger.Name = reader.GetString("name");
                        webmanger.webkind = reader.GetInt32("webkind");
                        webmanger.modify = reader.GetInt32("modif");
                        webmanger.delete = reader.GetInt32("delet");
                        webmanger.insert = reader.GetInt32("inser");
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
            if (webmanger == null)
            {
                return HttpNotFound();
            }
            return View(webmanger);
        }

        // GET: WebMangers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebMangers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( WebManger webManger)
        {
            if (ModelState.IsValid)
            {
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("INSERT INTO webmanger(account,password,name)VALUES" + "(" +
                "\"" + webManger.Account + "\"" + "," + "\"" + webManger.Password + "\"" + "," +"\"" + webManger.Name + "\""+ ")", mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Index");
            }

            return View(webManger);
        }

        // GET: WebMangers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebManger webManger = new WebManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from webmanger where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        webManger.Id = reader.GetInt32("id");
                        webManger.Account = reader.GetString("account");

                        webManger.Password = reader.GetString("password");
                        webManger.Name = reader.GetString("name");
                        webManger.webkind = reader.GetInt32("webkind");
                        webManger.modify = reader.GetInt32("modif");
                        webManger.delete = reader.GetInt32("delet");
                        webManger.insert = reader.GetInt32("inser");
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
            if (webManger == null)
            {
                return HttpNotFound();
            }
            return View(webManger);
        }

        // POST: WebMangers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WebManger webManger)
        {

            if (ModelState.IsValid)
            {
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("UPDATE webmanger set account=" + 
                "'" + webManger.Account + "'" + ",password=" + "'" + webManger.Password + "'" + ",name=" + 
                "'" + webManger.Name + "',webkind=" + webManger.webkind+ ",modif=" + webManger.modify+
                ",delet=" + webManger.delete+ ",inser=" + webManger.insert
                + " where id="+webManger.Id, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Index");
            }
            return View(webManger);
        }

        // GET: WebMangers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebManger webManger = new WebManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from webmanger where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        webManger.Id = reader.GetInt32("id");
                        webManger.Account = reader.GetString("account");

                        webManger.Password = reader.GetString("password");
                        webManger.Name = reader.GetString("name");
                        webManger.webkind = reader.GetInt32("webkind");
                        webManger.modify = reader.GetInt32("modif");
                        webManger.delete = reader.GetInt32("delet");
                        webManger.insert = reader.GetInt32("inser");
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
            if (webManger == null)
            {
                return HttpNotFound();
            }
            return View(webManger);
        }

        // POST: WebMangers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WebManger webManger = new WebManger();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("DELETE FROM webmanger where id=" + id, mysql);
            mysql.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
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
