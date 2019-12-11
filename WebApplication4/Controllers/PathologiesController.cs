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
    public class PathologiesController : Controller
    {
        private PathologyDbContext db = new PathologyDbContext();

        // GET: Pathologies
        public ActionResult Index()
        {
            return View(db.Pathologies.ToList());
        }

        // GET: Pathologies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pathology pathology = db.Pathologies.Find(id);
            if (pathology == null)
            {
                return HttpNotFound();
            }
            return View(pathology);
        }

        // GET: Pathologies/Create
        public ActionResult Create(int?id,string num,int refid,string checkname,int detailid)
        {
            Pathology pathology = new Pathology();
            pathology.rawid = refid;
            pathology.detailid = detailid;
            pathology.rawname = checkname;
            pathology.pathologynum = num;
            return View(pathology);
        }

        // POST: Pathologies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Pathology pathology)
        {
            
            if (ModelState.IsValid)
            {
                pathology.pathologynum= Request.Form["pathologynum"];
                string aa = Request.Form["detailid"];
                int bb = Convert.ToInt32(aa);
                MySqlConnection mysql = getMySqlConnection();
                string sql = "";
                sql = "INSERT INTO pathology(pathologynum,part,chronic,acute,atrophy,intestinal,dysplasia,lymphoid,pitepithelial,Mucosal)VALUES" + "('" +
                    pathology.pathologynum + "'," + 0 + "," + pathology.chronic + "," + pathology.acute + "," + pathology.atrophy + "," + pathology.intestinal + "," + pathology.dysplasia +
                    "," + pathology.lymphoid + "," +pathology.pitepithelial + "," +pathology.Mucosal+"),"+"('"+pathology.pathologynum+"'," +1 + "," + pathology.chronic1 + "," + pathology.acute1 +
                    "," + pathology.atrophy1 + "," + pathology.intestinal1 + "," + pathology.dysplasia1 +
                    "," + pathology.lymphoid1 + "," + pathology.pitepithelial1 + "," + pathology.Mucosal1 + ")";

                MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/"+bb, "Visits");
            }
            else
            {
                List<string> Keys = ModelState.Keys.ToList();
                //获取每一个key对应的ModelStateDictionary
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述输出到控制台
                    foreach (var error in errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }

            return View(pathology);
        }

        // GET: Pathologies/Edit/5
        public ActionResult Edit(int? id,string num,int detailid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DataTable dt = GetId(num);
            
            Pathology pathology = new Pathology();
            pathology.Id2 = Convert.ToInt32(dt.Rows[0][0]);
            pathology.id1 = Convert.ToInt32(dt.Rows[1][0]);
   
            DataTable dtContent1 = GetContent(pathology.Id2);
            pathology.pathologynum = num;
            pathology.detailid = detailid;

            pathology.part = Convert.ToInt32(dtContent1.Rows[0][2]);
            pathology.chronic = Convert.ToInt32(dtContent1.Rows[0][3]);
            pathology.acute = Convert.ToInt32(dtContent1.Rows[0][4]);
            pathology.atrophy = Convert.ToInt32(dtContent1.Rows[0][5]);
            pathology.intestinal = Convert.ToInt32(dtContent1.Rows[0][6]);
            pathology.dysplasia = Convert.ToInt32(dtContent1.Rows[0][7]);
            pathology.lymphoid = Convert.ToInt32(dtContent1.Rows[0][8]);
            pathology.pitepithelial = Convert.ToInt32(dtContent1.Rows[0][9]);
            pathology.Mucosal = Convert.ToInt32(dtContent1.Rows[0][10]);

            DataTable dtContent = GetContent(pathology.id1);
            pathology.part1 = Convert.ToInt32(dtContent.Rows[0][2]);
            pathology.chronic1 = Convert.ToInt32(dtContent.Rows[0][3]);
            pathology.acute1 = Convert.ToInt32(dtContent.Rows[0][4]);
            pathology.atrophy1 = Convert.ToInt32(dtContent.Rows[0][5]);
            pathology.intestinal1 = Convert.ToInt32(dtContent.Rows[0][6]);
            pathology.dysplasia1 = Convert.ToInt32(dtContent.Rows[0][7]);
            pathology.lymphoid1 = Convert.ToInt32(dtContent.Rows[0][8]);
            pathology.pitepithelial1 = Convert.ToInt32(dtContent.Rows[0][9]);
            pathology.Mucosal1 = Convert.ToInt32(dtContent.Rows[0][10]);

            return View(pathology);
        }

        // POST: Pathologies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Pathology pathology)
        {
            if (ModelState.IsValid)
            {
                pathology.pathologynum = Request.Form["pathologynum"];
                string aa = Request.Form["detailid"];
                int bb = Convert.ToInt32(aa);
                string a= Request.Form["Id2"];
                int b= Convert.ToInt32(a);
                UpdatePathlogy(b,pathology.pathologynum,0,pathology.chronic,pathology.acute,pathology.atrophy,
                    pathology.intestinal,pathology.dysplasia,pathology.lymphoid,pathology.pitepithelial,pathology.Mucosal);

                UpdatePathlogy(pathology.id1, pathology.pathologynum,1, pathology.chronic1, pathology.acute1, pathology.atrophy1,
                   pathology.intestinal1, pathology.dysplasia1, pathology.lymphoid1, pathology.pitepithelial1, pathology.Mucosal1);
                return RedirectToAction("Details/" + bb, "Visits");
            }
            return View(pathology);
        }

        // GET: Pathologies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pathology pathology = db.Pathologies.Find(id);
            if (pathology == null)
            {
                return HttpNotFound();
            }
            return View(pathology);
        }

        // POST: Pathologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pathology pathology = db.Pathologies.Find(id);
            db.Pathologies.Remove(pathology);
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

        public static DataTable GetId(string num)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select id from pathology where pathologynum='" + num+"'";
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        public static DataTable GetContent(int id)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = "select * from pathology where id=" +id;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
            DataTable dt = new DataTable();
            command.Fill(dt);
            return dt;
        }
        public static void UpdatePathlogy(int id,string pathologynum,int part,int chronic,int acute,
            int atrophy,int intestinal,int dysplasia,int lymphoid,int pitepithelial,int Mucosal)
        {
            MySqlConnection mysql = getMySqlConnection();
            string sql;
            sql = " UPDATE pathology set pathologynum='" + pathologynum + "',part=" + part + ",chronic=" + chronic + ",acute=" +
                acute + ",atrophy=" + atrophy + ",intestinal=" + intestinal + ",dysplasia=" + dysplasia + ",lymphoid=" +
                lymphoid + ",pitepithelial=" + pitepithelial + ",Mucosal=" + Mucosal+" where id="+id;
            MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
            mysql.Open();
            MySqlDataAdapter command = new MySqlDataAdapter(mySqlCommand);
            mySqlCommand.ExecuteNonQuery();
            mysql.Close();
           
            
        }
    }
}
