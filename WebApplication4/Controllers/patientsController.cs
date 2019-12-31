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
    public class patientsController : Controller
    {
        private patientDbContext db = new patientDbContext();

        // GET: patients
        public ActionResult Index(int page = 1)
        {
            List<patient> listpatient = new List<patient>();
            MySqlConnection mysql = getMySqlConnection();

            MySqlCommand mySqlCommand = getSqlCommand("select * from medical_patient", mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            var i = 0;
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        patient patient = new patient();

                        patient.id = reader.GetInt32("id");
                        patient.stage = reader.GetInt32("stage");

                        patient.age = reader.GetInt32("age");
                        patient.insurance = reader.GetString("insurance");
                        patient.height = reader.GetInt32("height");
                        patient.weight = reader.GetInt32("weight");
                        patient.area = reader.GetString("area");
                        patient.bestcase = reader.GetInt32("bestcase");
                        patient.feepredicate = reader.GetInt32("feepredicate");
                        listpatient.Add(patient);
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
            var data = listpatient
                 .OrderBy(p => p.id).ToPagedList(page, pagesize);

            return View(data);
        }

        // GET: patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = new patient();
           
            MySqlConnection mysql = getMySqlConnection();

            MySqlCommand mySqlCommand = getSqlCommand("select * from medical_patient", mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
           
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        

                        patient.id = reader.GetInt32("id");
                        patient.stage = reader.GetInt32("stage");

                        patient.age = reader.GetInt32("age");
                        patient.insurance = reader.GetString("insurance");
                        patient.height = reader.GetInt32("height");
                        patient.weight = reader.GetInt32("weight");
                        patient.area = reader.GetString("area");

                        patient.bestcase = reader.GetInt32("bestcase");
                        patient.feepredicate = reader.GetInt32("feepredicate");


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
            return View(patient);
        }

        // GET: patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,stage,age,insurance,height,area,weight")] patient patient)
        {
            if (ModelState.IsValid)
            {
                db.patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patient);
        }

        // GET: patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,stage,age,insurance,height,area,weight")] patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            patient patient = db.patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            patient patient = db.patients.Find(id);
            db.patients.Remove(patient);
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
