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
    public class OperateTreatmentsController : Controller
    {
        private OperateTreatmentDbContext db = new OperateTreatmentDbContext();

        // GET: OperateTreatments
        public ActionResult Index()
        {
            return View(db.Operatetreatments.ToList());
        }

        // GET: OperateTreatments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperateTreatment operateTreatment = db.Operatetreatments.Find(id);
            if (operateTreatment == null)
            {
                return HttpNotFound();
            }
            return View(operateTreatment);
        }

        // GET: OperateTreatments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OperateTreatments/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,visit,scannum,conditions,time,content,pic1,pic2,pic,isfollowup")] OperateTreatment operateTreatment)
        {
            if (ModelState.IsValid)
            {
				
				string sql = "INSERT INTO treatment ";
				
				MySqlConnection mysql = getMySqlConnection();
				MySqlCommand mySqlCommand = getSqlCommand(sql, mysql);
				mysql.Open();
				MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
				mySqlCommand.ExecuteNonQuery();
				mysql.Close();
				//return RedirectToAction("Details/" + b, "Visits");
				return RedirectToAction("Index");
            }

            return View(operateTreatment);
        }

        // GET: OperateTreatments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperateTreatment operateTreatment = db.Operatetreatments.Find(id);
            if (operateTreatment == null)
            {
                return HttpNotFound();
            }
            return View(operateTreatment);
        }

        // POST: OperateTreatments/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,visit,scannum,conditions,time,content,pic1,pic2,pic,isfollowup")] OperateTreatment operateTreatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(operateTreatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(operateTreatment);
        }

        // GET: OperateTreatments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OperateTreatment operateTreatment = db.Operatetreatments.Find(id);
            if (operateTreatment == null)
            {
                return HttpNotFound();
            }
            return View(operateTreatment);
        }

        // POST: OperateTreatments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OperateTreatment operateTreatment = db.Operatetreatments.Find(id);
            db.Operatetreatments.Remove(operateTreatment);
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
