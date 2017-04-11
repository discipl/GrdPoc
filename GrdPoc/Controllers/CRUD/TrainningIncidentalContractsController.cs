using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GrdPoc.Models;
using GrdPoc.Models.Entities;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class TrainningIncidentalContractsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrainningIncidentalContracts
        public async Task<ActionResult> Index()
        {
            var trainningIncidentalContracts = db.TrainningIncidentalContracts.Include(t => t.IncidentalContracController).Include(t => t.IncidentalContracOwner).Include(t => t.IncidentalContracProvider).Include(t => t.IncidentalContract);
            return View(await trainningIncidentalContracts.ToListAsync());
        }

        // GET: TrainningIncidentalContracts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainningIncidentalContract trainningIncidentalContract = await db.TrainningIncidentalContracts.FindAsync(id);
            if (trainningIncidentalContract == null)
            {
                return HttpNotFound();
            }
            return View(trainningIncidentalContract);
        }

        // GET: TrainningIncidentalContracts/Create
        public ActionResult Create()
        {
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle");
            return View();
        }

        // POST: TrainningIncidentalContracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TrainningIncidentalContractId,ServiceIncidentalContractPurpose,ServiceIncidentalContractHours,ServiceIncidentalContractProfile,IncidentalContractId,IncidentalContractTitle,IncidentalContracType,IncidentalContracStatus,IncidentalContracOwnerId,IncidentalContracControllerId,IncidentalContracProviderId,IncidentalContracDuration,IncidentalContracTimeframeStart,IncidentalContracTimeframeEnd,OwnerTermsAgreementDate,ProviderTermsAgreementDate,ControllerDeliverenceConfirmationDate,ProviderDeliverenceConfirmationDate")] TrainningIncidentalContract trainningIncidentalContract)
        {
            if (ModelState.IsValid)
            {
                db.TrainningIncidentalContracts.Add(trainningIncidentalContract);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", trainningIncidentalContract.IncidentalContractId);
            return View(trainningIncidentalContract);
        }

        // GET: TrainningIncidentalContracts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainningIncidentalContract trainningIncidentalContract = await db.TrainningIncidentalContracts.FindAsync(id);
            if (trainningIncidentalContract == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", trainningIncidentalContract.IncidentalContractId);
            return View(trainningIncidentalContract);
        }

        // POST: TrainningIncidentalContracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TrainningIncidentalContractId,ServiceIncidentalContractPurpose,ServiceIncidentalContractHours,ServiceIncidentalContractProfile,IncidentalContractId,IncidentalContractTitle,IncidentalContracType,IncidentalContracStatus,IncidentalContracOwnerId,IncidentalContracControllerId,IncidentalContracProviderId,IncidentalContracDuration,IncidentalContracTimeframeStart,IncidentalContracTimeframeEnd,OwnerTermsAgreementDate,ProviderTermsAgreementDate,ControllerDeliverenceConfirmationDate,ProviderDeliverenceConfirmationDate")] TrainningIncidentalContract trainningIncidentalContract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainningIncidentalContract).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", trainningIncidentalContract.IncidentalContractId);
            return View(trainningIncidentalContract);
        }

        // GET: TrainningIncidentalContracts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainningIncidentalContract trainningIncidentalContract = await db.TrainningIncidentalContracts.FindAsync(id);
            if (trainningIncidentalContract == null)
            {
                return HttpNotFound();
            }
            return View(trainningIncidentalContract);
        }

        // POST: TrainningIncidentalContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TrainningIncidentalContract trainningIncidentalContract = await db.TrainningIncidentalContracts.FindAsync(id);
            db.TrainningIncidentalContracts.Remove(trainningIncidentalContract);
            await db.SaveChangesAsync();
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
