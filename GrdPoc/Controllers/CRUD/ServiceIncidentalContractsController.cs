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
    public class ServiceIncidentalContractsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiceIncidentalContracts
        public ActionResult Index()
        {
            var serviceIncidentalContracts = db.ServiceIncidentalContracts.Include(s => s.IncidentalContracController).Include(s => s.IncidentalContracOwner).Include(s => s.IncidentalContracProvider).Include(s => s.IncidentalContract).Include(s => s.IncidentalContracType);
            return View(serviceIncidentalContracts.ToList());
        }

        // GET: ServiceIncidentalContracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIncidentalContract serviceIncidentalContract = db.ServiceIncidentalContracts.Find(id);
            if (serviceIncidentalContract == null)
            {
                return HttpNotFound();
            }
            return View(serviceIncidentalContract);
        }

        // GET: ServiceIncidentalContracts/Create
        public ActionResult Create()
        {
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle");
            ViewBag.IncidentalContracTypeId = new SelectList(db.IncidentalContracTypes, "IncidentalContracTypeId", "IncidentalContracTypeTitle");
            return View();
        }

        // POST: ServiceIncidentalContracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceIncidentalContractId,ServiceIncidentalContractPurpose,ServiceIncidentalContractHours,ServiceIncidentalContractProfile,IncidentalContractId,IncidentalContractTitle,IncidentalContracTypeId,IncidentalContracStatus,IncidentalContracOwnerId,IncidentalContracControllerId,IncidentalContracProviderId,IncidentalContracDuration,IncidentalContracTimeframeStart,IncidentalContracTimeframeEnd,OwnerTermsAgreementDate,ProviderTermsAgreementDate,ControllerDeliverenceConfirmationDate,ProviderDeliverenceConfirmationDate")] ServiceIncidentalContract serviceIncidentalContract)
        {
            if (ModelState.IsValid)
            {
                db.ServiceIncidentalContracts.Add(serviceIncidentalContract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", serviceIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", serviceIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", serviceIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", serviceIncidentalContract.IncidentalContractId);
            ViewBag.IncidentalContracTypeId = new SelectList(db.IncidentalContracTypes, "IncidentalContracTypeId", "IncidentalContracTypeTitle", serviceIncidentalContract.IncidentalContracTypeId);
            return View(serviceIncidentalContract);
        }

        // GET: ServiceIncidentalContracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIncidentalContract serviceIncidentalContract = db.ServiceIncidentalContracts.Find(id);
            if (serviceIncidentalContract == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", serviceIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", serviceIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", serviceIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", serviceIncidentalContract.IncidentalContractId);
            ViewBag.IncidentalContracTypeId = new SelectList(db.IncidentalContracTypes, "IncidentalContracTypeId", "IncidentalContracTypeTitle", serviceIncidentalContract.IncidentalContracTypeId);
            return View(serviceIncidentalContract);
        }

        // POST: ServiceIncidentalContracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceIncidentalContractId,ServiceIncidentalContractPurpose,ServiceIncidentalContractHours,ServiceIncidentalContractProfile,IncidentalContractId,IncidentalContractTitle,IncidentalContracTypeId,IncidentalContracStatus,IncidentalContracOwnerId,IncidentalContracControllerId,IncidentalContracProviderId,IncidentalContracDuration,IncidentalContracTimeframeStart,IncidentalContracTimeframeEnd,OwnerTermsAgreementDate,ProviderTermsAgreementDate,ControllerDeliverenceConfirmationDate,ProviderDeliverenceConfirmationDate")] ServiceIncidentalContract serviceIncidentalContract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceIncidentalContract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", serviceIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", serviceIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", serviceIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", serviceIncidentalContract.IncidentalContractId);
            ViewBag.IncidentalContracTypeId = new SelectList(db.IncidentalContracTypes, "IncidentalContracTypeId", "IncidentalContracTypeTitle", serviceIncidentalContract.IncidentalContracTypeId);
            return View(serviceIncidentalContract);
        }

        // GET: ServiceIncidentalContracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceIncidentalContract serviceIncidentalContract = db.ServiceIncidentalContracts.Find(id);
            if (serviceIncidentalContract == null)
            {
                return HttpNotFound();
            }
            return View(serviceIncidentalContract);
        }

        // POST: ServiceIncidentalContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceIncidentalContract serviceIncidentalContract = db.ServiceIncidentalContracts.Find(id);
            db.ServiceIncidentalContracts.Remove(serviceIncidentalContract);
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
