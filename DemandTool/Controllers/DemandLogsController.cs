using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemandTool.MVC.Context;
using DemandTool.MVC.Models;
using Newtonsoft.Json;

namespace DemandTool.MVC.Controllers
{
    [Authorize]
    public class DemandLogsController : Controller
    {
        private DefaultDBContext db = new DefaultDBContext();

        // GET: DemandLogs
        public ActionResult Index()
        {
            var demandLogs = db.DemandLogs.Include(d => d.Demand);
            return View(demandLogs.ToList());
        }

        // GET: DemandLogs/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<DemandLog> demandLogs = db.DemandLogs.Where(dL => dL.DemandId == id).ToList();
            if (demandLogs == null)
            {
                return HttpNotFound();
            }
               var result= Json(JsonConvert.SerializeObject( demandLogs), JsonRequestBehavior.AllowGet);
            return result;
        }

        // GET: DemandLogs/Create
        public ActionResult Create()
        {
            ViewBag.DemandId = new SelectList(db.Demands, "Id", "DemandNumber");
            return View();
        }
        //public ActionResult GetByDemandID(long? demandid)
        //{
        //    var result = db.DemandLogs.Include(d => d.Demand).Where(r => r.DemandId == demandid).ToList();
        //    db.DemandLogs.Add(
        //    db.SaveChanges();

        //    return View();
        //}
        //// POST: DemandLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AssignedTeam,TeamStatus,DemandId")] DemandLog demandLogs)
        {
            if (ModelState.IsValid)
            {
                db.DemandLogs.Add(demandLogs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DemandId = new SelectList(db.Demands, "Id", "DemandNumber", demandLogs.DemandId);
            return View(demandLogs);
        }

        // GET: DemandLogs/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandLog demandLogs = db.DemandLogs.Find(id);
            if (demandLogs == null)
            {
                return HttpNotFound();
            }
            ViewBag.DemandId = new SelectList(db.Demands, "Id", "DemandNumber", demandLogs.DemandId);
            return View(demandLogs);
        }

        // POST: DemandLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AssignedTeam,TeamStatus,DemandId")] DemandLog demandLogs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(demandLogs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DemandId = new SelectList(db.Demands, "Id", "DemandNumber", demandLogs.DemandId);
            return View(demandLogs);
        }

        // GET: DemandLogs/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DemandLog demandLogs = db.DemandLogs.Find(id);
            if (demandLogs == null)
            {
                return HttpNotFound();
            }
            return View(demandLogs);
        }

        // POST: DemandLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DemandLog demandLogs = db.DemandLogs.Find(id);
            db.DemandLogs.Remove(demandLogs);
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
