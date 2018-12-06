using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using DemandTool.MVC.Context;
using DemandTool.MVC.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        //public ActionResult IndexData()
        //    {

        //    var result = db.Demands.ToList();
        //      //var result2 =  Json(JsonConvert.SerializeObject(result.ToArray()), JsonRequestBehavior.AllowGet);

        //    var serializer = new JavaScriptSerializer();
        //    var serializedResult = serializer.Serialize(result);
        //    return Json (serializedResult);


        //}
        public ActionResult IndexData()
        {
            
            var result = (db.Demands.ToList()).ToArray();
            var result2 =  Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);

            //var serializer = new JavaScriptSerializer();
            //var serializedResult = serializer.Serialize(result);
            return result2;

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


        public ActionResult ExportToExcel()
        {
            // Step 1 - get the data from database
            var data = db.Demands.Select(r => new { r.Id, r.DemandDesc , r.SubmissionDate,r.DemandNumber, r.DemandStatus, r.Priority, r.Phase }).ToList();
   
            


            // instantiate the GridView control from System.Web.UI.WebControls namespace
            // set the data source
            GridView gridview = new GridView();
            gridview.DataSource = data;
            gridview.DataBind();

            // Clear all the content from the current response
            Response.ClearContent();
            Response.Buffer = true;
            // set the header
            Response.AddHeader("content-disposition", "attachment; filename = Demands.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            // create HtmlTextWriter object with StringWriter
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // render the GridView to the HtmlTextWriter
                    gridview.RenderControl(htw);
                    // Output the GridView content saved into StringWriter
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index");
        }



        public ActionResult ExportHops()
        {

            
            var data = db.DemandLogs.ToList();
            // var data = db.DemandLogs.Select(r => new { r.Id, r.DemandDesc, r.SubmissionDate, r.DemandNumber, r.DemandStatus, r.Priority, r.Phase }).ToList();

            GridView gridview = new GridView();
            gridview.DataSource = data;
            gridview.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename = Demand logs.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    gridview.RenderControl(htw);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
