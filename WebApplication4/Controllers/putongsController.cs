using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class putongsController : Controller
    {
        private putongDbContext db = new putongDbContext();

        // GET: putongs
        public ActionResult Index()
        {
            return View(db.Putongs.ToList());
        }

        // GET: qiyes
        public ActionResult Index1(int page = 1)
        {
            List<putong> listqiyes = new List<putong>();
           

            return View(listqiyes);

        }

        // GET: putongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            putong putong = db.Putongs.Find(id);
            if (putong == null)
            {
                return HttpNotFound();
            }
            return View(putong);
        }

        // GET: putongs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: putongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content")] putong putong)
        {
            if (ModelState.IsValid)
            {
                db.Putongs.Add(putong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(putong);
        }

        // GET: putongs/Edit/5
        public ActionResult Edit(int id=1)
        {
          
            return View();
        }

        // POST: putongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content")] putong putong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(putong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(putong);
        }

        // GET: putongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            putong putong = db.Putongs.Find(id);
            if (putong == null)
            {
                return HttpNotFound();
            }
            return View(putong);
        }

        // POST: putongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            putong putong = db.Putongs.Find(id);
            db.Putongs.Remove(putong);
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
