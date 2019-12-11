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
    public class DiseasesController : Controller
    {
        private DiseaseDbContext db = new DiseaseDbContext();

        // GET: Diseases
        public ActionResult Index()
        {
            return View(db.Diseases.ToList());
        }

        // GET: Diseases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // GET: Diseases/Create
        public ActionResult Create(int? id,string name,string phone,int refid)
        {
            Disease disease = new Disease();
            disease.RefId = refid;
            disease.Name = name;
            disease.Phone = phone;
            return View();
        }

        // POST: Diseases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RefId,Name,Phone,DiseaseId,Cardiovascular,Breathing,URM,SportsOthers,Specific,Gastric,Specificgastric,Allergy,Specificallergy")] Disease disease)
        {
            if (ModelState.IsValid)
            {
                string a = Request.Form["RefId"];
                int b = Convert.ToInt32(a);
                disease.Phone = Request.Form["Phone"];
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("INSERT INTO disease(phone,disease,cardiovascular,breathing,URM,SportsOthers,specifics,gastric,specificgastric,allergy,specificallergy)VALUES" + "(" +
                "'"+disease.Phone+"',"+disease.DiseaseId + "," + disease.Cardiovascular + "," + disease.Breathing + "," +disease.URM+","+
                disease.SportsOthers + "," + "\"" + disease.Specific + "\"" + "," + disease.Gastric + "," + "\"" + disease.Specificgastric + "\"" + "," + disease.Allergy + "," + "\"" + disease.Specificallergy + "\"" 
                  + ")", mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/" + b, "UserMangers");
            }

            return View(disease);
        }

        // GET: Diseases/Edit/5
        public ActionResult Edit(int? id,int refId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = new Disease();
            MySqlConnection mysql = getMySqlConnection();
            MySqlCommand mySqlCommand = getSqlCommand("select * from disease where id=" + id, mysql);
            mysql.Open();
            MySqlDataReader reader = mySqlCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        disease.RefId = refId;
                        disease.Id = reader.GetInt32("id");
                        disease.DiseaseId = reader.GetInt32("disease");
                        disease.Cardiovascular = reader.GetInt32("cardiovascular");

                        disease.Breathing = reader.GetInt32("breathing");
                        disease.URM = reader.GetInt32("URM");
                        disease.SportsOthers = reader.GetInt32("SportsOthers");
                        disease.Specific = reader.GetString("specifics");


                        disease.Gastric = reader.GetInt32("gastric");

                        disease.Specificgastric = reader.GetString("specificgastric");
                       
                        disease.Allergy = reader.GetInt32("allergy");
                        disease.Specificallergy = reader.GetString("specificallergy");
                        
                       

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
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RefId,Id,DiseaseId,Cardiovascular,Breathing,URM,SportsOthers,Specific,Gastric,Specificgastric,Allergy,Specificallergy")] Disease disease)
        {
            
            if (ModelState.IsValid)
            {
                string a = Request.Form["RefId"];
                int b = Convert.ToInt32(a);
                MySqlConnection mysql = getMySqlConnection();
                MySqlCommand mySqlCommand = getSqlCommand("UPDATE disease set disease=" +
                 disease.DiseaseId + ",cardiovascular=" +  disease.Cardiovascular  + ",breathing=" +  disease.Breathing  + ",URM=" +
                 disease.URM+ ",SportsOthers=" + disease.SportsOthers+ ",specifics=" +"'"+disease.Specific+ "',gastric=" +disease.Gastric+ ",specificgastric="+
                 "'"+disease.Specificgastric+ "',allergy=" +disease.Allergy+ ",specificallergy="+"'"+disease.Specificallergy+ "'"+ "where id=" + disease.Id, mysql);
                mysql.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(mySqlCommand);
                mySqlCommand.ExecuteNonQuery();
                mysql.Close();
                return RedirectToAction("Details/"+b, "UserMangers");

            }
            return View(disease);
        }

        // GET: Diseases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disease disease = db.Diseases.Find(id);
            if (disease == null)
            {
                return HttpNotFound();
            }
            return View(disease);
        }

        // POST: Diseases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disease disease = db.Diseases.Find(id);
            db.Diseases.Remove(disease);
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
