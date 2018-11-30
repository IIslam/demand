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
    public class DemandsController : Controller
    {
        private DefaultDBContext db = new DefaultDBContext();

        // GET: Demands
        public ActionResult Index()
        {
            return View(db.Demands.ToList());
        }

        // GET: Demands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demand demand = db.Demands.Include(d=>d.DemandLogs).ToList().Find(d => d.Id == id);
            if (demand == null)
            {
                return HttpNotFound();
            }
            return View(demand);
        }

        // GET: Demands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Demands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Demand demand)
        {
            if (ModelState.IsValid)
            {
                demand.DemandLogs = new List<DemandLog>()
                {
                    new DemandLog()
                    {
                        AssignedTeam = AssignedTeam.ProductMgt,
                        TeamStatus = TeamStatus.Assigned,
                        DemandStatus= demand.DemandStatus,
                        Phase = demand.Phase,
                        Priority = demand.Priority,
                        RAG = demand.RAG,
                        ServiceLine = demand.ServiceLine,
                        UpdatedDate = DateTime.Now
                    }
                };
                db.Demands.Add(demand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(demand);
        }

        // GET: Demands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demand demand = db.Demands.Find(id);
            if (demand == null)
            {
                return HttpNotFound();
            }
            return View(demand);
        }

        // POST: Demands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(DemandModel demand)
        {
            if (ModelState.IsValid)
            {
                var dbDemand = db.Demands.Include(d=>d.DemandLogs).First(d=>d.Id == demand.Id);
                dbDemand.Priority = demand.Priority;
                dbDemand.RAG = demand.RAG;
                dbDemand.DemandStatus = demand.DemandStatus;
                dbDemand.ServiceLine = demand.ServiceLine;
                dbDemand.Phase = demand.Phase;
                var newDemandLog = new DemandLog()
                {
                    Priority = demand.Priority,
                    RAG = demand.RAG,
                    DemandStatus = demand.DemandStatus,
                    ServiceLine = demand.ServiceLine,
                    Phase = demand.Phase,
                    AssignedTeam = demand.AssignedTeam,
                    TeamStatus = demand.TeamStatus,
                    Comments = demand.Comments,
                    UpdatedDate = DateTime.Now,
                    DemandId = demand.Id
                };
                dbDemand.DemandLogs.Add(newDemandLog);
                db.SaveChanges();
                //TODO:create DemandDTO
                newDemandLog.Demand = null;
                return Json(JsonConvert.SerializeObject(newDemandLog));
            }
            return Json(JsonConvert.SerializeObject(new { errorMsg = "invalid Object" }));
        }

        // GET: Demands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demand demand = db.Demands.Find(id);
            if (demand == null)
            {
                return HttpNotFound();
            }
            return View(demand);
        }

        // POST: Demands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Demand demand = db.Demands.Find(id);
            db.Demands.Remove(demand);
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
