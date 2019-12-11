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
    public class WorkflowsController : Controller
    {
        private WorkflowDbcontext db = new WorkflowDbcontext();

        // GET: Workflows
        public ActionResult Index()
        {
            return View(db.Workflows.ToList());
        }

        // GET: Workflows/Details/5
        public ActionResult Details(string id)
        {
            Workflow workflow = new Workflow();

            return View(workflow);
        }

        // GET: Workflows/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workflows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Pic1,Pic2,Pic3")] Workflow workflow)
        {
            if (ModelState.IsValid)
            {
                db.Workflows.Add(workflow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workflow);
        }

        // GET: Workflows/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workflow workflow = db.Workflows.Find(id);
            if (workflow == null)
            {
                return HttpNotFound();
            }
            return View(workflow);
        }

        // POST: Workflows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Pic1,Pic2,Pic3")] Workflow workflow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workflow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workflow);
        }

        // GET: Workflows/Delete/5
        public ActionResult Delete(string id)
        {
           
            Workflow workflow =new Workflow();
           
            return View(workflow);
        }

        // POST: Workflows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Workflow workflow = db.Workflows.Find(id);
            db.Workflows.Remove(workflow);
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
