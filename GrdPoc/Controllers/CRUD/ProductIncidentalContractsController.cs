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
    public class ProductIncidentalContractsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductIncidentalContracts
        public ActionResult Index()
        {
            var productIncidentalContracts = db.ProductIncidentalContracts.Include(p => p.IncidentalContracController).Include(p => p.IncidentalContracOwner).Include(p => p.IncidentalContracProvider).Include(p => p.IncidentalContract);
            return View(productIncidentalContracts.ToList());
        }

        // GET: ProductIncidentalContracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductIncidentalContract productIncidentalContract = db.ProductIncidentalContracts.Find(id);
            if (productIncidentalContract == null)
            {
                return HttpNotFound();
            }
            return View(productIncidentalContract);
        }

        // GET: ProductIncidentalContracts/Create
        public ActionResult Create()
        {
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle");
            return View();
        }

        // POST: ProductIncidentalContracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainningIncidentalContractId,IncidentalContractId,IncidentalContractTitle,IncidentalContracType,IncidentalContracStatus,IncidentalContracOwnerId,IncidentalContracControllerId,IncidentalContracProviderId,IncidentalContracDuration,IncidentalContracTimeframeStart,IncidentalContracTimeframeEnd,OwnerTermsAgreementDate,ProviderTermsAgreementDate,ControllerDeliverenceConfirmationDate,ProviderDeliverenceConfirmationDate")] ProductIncidentalContract productIncidentalContract)
        {
            if (ModelState.IsValid)
            {
                db.ProductIncidentalContracts.Add(productIncidentalContract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", productIncidentalContract.IncidentalContractId);
            return View(productIncidentalContract);
        }

        // GET: ProductIncidentalContracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductIncidentalContract productIncidentalContract = db.ProductIncidentalContracts.Find(id);
            if (productIncidentalContract == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", productIncidentalContract.IncidentalContractId);
            return View(productIncidentalContract);
        }

        // POST: ProductIncidentalContracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainningIncidentalContractId,IncidentalContractId,IncidentalContractTitle,IncidentalContracType,IncidentalContracStatus,IncidentalContracOwnerId,IncidentalContracControllerId,IncidentalContracProviderId,IncidentalContracDuration,IncidentalContracTimeframeStart,IncidentalContracTimeframeEnd,OwnerTermsAgreementDate,ProviderTermsAgreementDate,ControllerDeliverenceConfirmationDate,ProviderDeliverenceConfirmationDate")] ProductIncidentalContract productIncidentalContract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productIncidentalContract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", productIncidentalContract.IncidentalContractId);
            return View(productIncidentalContract);
        }

        // GET: ProductIncidentalContracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductIncidentalContract productIncidentalContract = db.ProductIncidentalContracts.Find(id);
            if (productIncidentalContract == null)
            {
                return HttpNotFound();
            }
            return View(productIncidentalContract);
        }

        // POST: ProductIncidentalContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductIncidentalContract productIncidentalContract = db.ProductIncidentalContracts.Find(id);
            db.ProductIncidentalContracts.Remove(productIncidentalContract);
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
