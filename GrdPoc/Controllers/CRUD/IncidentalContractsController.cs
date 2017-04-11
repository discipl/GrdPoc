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
    public class IncidentalContractsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IncidentalContracts
        public ActionResult Index()
        {
            var incidentalContracts = db.IncidentalContracts.Include(i => i.OwnerMunicipalEntity).Include(i => i.ProviderMunicipalEntity);
            return View(incidentalContracts.ToList());
        }

        // GET: IncidentalContracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);
            if (incidentalContract == null)
            {
                return HttpNotFound();
            }
            return View(incidentalContract);
        }

        // GET: IncidentalContracts/Create
        public ActionResult Create()
        {
            ViewBag.OwnerMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName");
            ViewBag.ProviderMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName");
            return View();
        }

        // POST: IncidentalContracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentalContractId,ContractTitle,IncidentalContracType,OwnerMunicipalEntityId,IncidentalContracOwnerId,IncidentalContracOwnerName,IncidentalContracControllerId,IncidentalContracControllerName,ProviderMunicipalEntityId,IncidentalContracProviderId,IncidentalContracProviderName,IncidentalContracInvoiceNumber")] IncidentalContract incidentalContract)
        {
            if (ModelState.IsValid)
            {
                db.IncidentalContracts.Add(incidentalContract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", incidentalContract.OwnerMunicipalEntityId);
            ViewBag.ProviderMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", incidentalContract.ProviderMunicipalEntityId);
            return View(incidentalContract);
        }

        // GET: IncidentalContracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);
            if (incidentalContract == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", incidentalContract.OwnerMunicipalEntityId);
            ViewBag.ProviderMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", incidentalContract.ProviderMunicipalEntityId);
            return View(incidentalContract);
        }

        // POST: IncidentalContracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentalContractId,ContractTitle,IncidentalContracType,OwnerMunicipalEntityId,IncidentalContracOwnerId,IncidentalContracOwnerName,IncidentalContracControllerId,IncidentalContracControllerName,ProviderMunicipalEntityId,IncidentalContracProviderId,IncidentalContracProviderName,IncidentalContracInvoiceNumber")] IncidentalContract incidentalContract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentalContract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", incidentalContract.OwnerMunicipalEntityId);
            ViewBag.ProviderMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", incidentalContract.ProviderMunicipalEntityId);
            return View(incidentalContract);
        }

        // GET: IncidentalContracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);
            if (incidentalContract == null)
            {
                return HttpNotFound();
            }
            return View(incidentalContract);
        }

        // POST: IncidentalContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);
            db.IncidentalContracts.Remove(incidentalContract);
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
