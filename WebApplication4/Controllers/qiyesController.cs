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
    public class qiyesController : Controller
    {
        private qiyeDbContext db = new qiyeDbContext();

        // GET: qiyes
        public ActionResult Index(int page = 1)
        {
            List<qiye> listqiyes = new List<qiye>();
            const int pagesize = 8;
            var data = listqiyes
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);

            return View(data);
            
        }

        // GET: qiyes
        public ActionResult Index1(int page = 1)
        {
            List<qiye> listqiyes = new List<qiye>();
            const int pagesize = 8;
            var data = listqiyes
                 .OrderBy(p => p.Id).ToPagedList(page, pagesize);

            return View(data);

        }


        // GET: qiyes/Details/5
        public ActionResult Details(int id=1)
        {

            qiye qiye = new qiye();
            
            return View(qiye);
        }

        // GET: qiyes/Details1/5
        public ActionResult Details1(int id = 1)
        {

            qiye qiye = new qiye();

            return View(qiye);
        }

        // GET: qiyes/Details2/5
        public ActionResult Details2(int id = 1)
        {

            qiye qiye = new qiye();

            return View(qiye);
        }

        // GET: qiyes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: qiyes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content")] qiye qiye)
        {
            if (ModelState.IsValid)
            {
                db.Qiyes.Add(qiye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qiye);
        }

        // GET: qiyes/Edit
        public ActionResult Edit()
        {
           
            return View();
        }

        // POST: qiyes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content")] qiye qiye)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qiye).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qiye);
        }

        // GET: qiyes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qiye qiye = db.Qiyes.Find(id);
            if (qiye == null)
            {
                return HttpNotFound();
            }
            return View(qiye);
        }

        // POST: qiyes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            qiye qiye = db.Qiyes.Find(id);
            db.Qiyes.Remove(qiye);
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
    }
}
