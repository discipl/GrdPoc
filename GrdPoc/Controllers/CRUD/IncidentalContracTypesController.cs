using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GrdPoc.Models;
using GrdPoc.Models.Entities;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class IncidentalContracTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IncidentalContracTypes
        public ActionResult Index()
        {
            var incidentalContracTypes = db.IncidentalContracTypes.Include(i => i.SmartContract);
            return View(incidentalContracTypes.ToList());
        }

        // GET: IncidentalContracTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContracType incidentalContracType = db.IncidentalContracTypes.Find(id);
            if (incidentalContracType == null)
            {
                return HttpNotFound();
            }
            return View(incidentalContracType);
        }

        // GET: IncidentalContracTypes/Create
        public ActionResult Create()
        {
            ViewBag.SmartContractId = new SelectList(db.SmartContracts, "SmartContractId", "SmartContractName");
            return View();
        }

        // POST: IncidentalContracTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentalContracTypeId,IncidentalContracTypeTitle,IncidentalContracDescription,IncidentalContracMvcController,SmartContractId,IncidentalContracTypeActive")] IncidentalContracType incidentalContracType)
        {
            if (ModelState.IsValid)
            {
                db.IncidentalContracTypes.Add(incidentalContracType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SmartContractId = new SelectList(db.SmartContracts, "SmartContractId", "SmartContractName", incidentalContracType.SmartContractId);
            return View(incidentalContracType);
        }

        // GET: IncidentalContracTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContracType incidentalContracType = db.IncidentalContracTypes.Find(id);
            if (incidentalContracType == null)
            {
                return HttpNotFound();
            }
            ViewBag.SmartContractId = new SelectList(db.SmartContracts, "SmartContractId", "SmartContractName", incidentalContracType.SmartContractId);
            return View(incidentalContracType);
        }

        // POST: IncidentalContracTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentalContracTypeId,IncidentalContracTypeTitle,IncidentalContracDescription,IncidentalContracMvcController,SmartContractId,IncidentalContracTypeActive")] IncidentalContracType incidentalContracType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentalContracType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SmartContractId = new SelectList(db.SmartContracts, "SmartContractId", "SmartContractName", incidentalContracType.SmartContractId);
            return View(incidentalContracType);
        }

        // GET: IncidentalContracTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContracType incidentalContracType = db.IncidentalContracTypes.Find(id);
            if (incidentalContracType == null)
            {
                return HttpNotFound();
            }
            return View(incidentalContracType);
        }

        // POST: IncidentalContracTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentalContracType incidentalContracType = db.IncidentalContracTypes.Find(id);
            db.IncidentalContracTypes.Remove(incidentalContracType);
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
