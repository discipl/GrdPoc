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
    public class SmartContractsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SmartContracts
        public ActionResult Index()
        {
            var smartContracts = db.SmartContracts.Include(s => s.BlockChainDeployAccount);
            return View(smartContracts.ToList());
        }

        // GET: SmartContracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SmartContract smartContract = db.SmartContracts.Find(id);
            if (smartContract == null)
            {
                return HttpNotFound();
            }
            return View(smartContract);
        }

        // GET: SmartContracts/Create
        public ActionResult Create()
        {
            ViewBag.BlockChainAccountId = new SelectList(db.BlockchainAccounts, "BlockChainAccountId", "AccountName");
            return View();
        }

        // POST: SmartContracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SmartContractId,SmartContractName,SmartContractByteCode,SmartContractAbi,SmartContractAddress,SmartContractCode,SmartContractActive,BlockChainAccountId")] SmartContract smartContract)
        {
            if (ModelState.IsValid)
            {
                db.SmartContracts.Add(smartContract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BlockChainAccountId = new SelectList(db.BlockchainAccounts, "BlockChainAccountId", "AccountName", smartContract.BlockChainAccountId);
            return View(smartContract);
        }

        // GET: SmartContracts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SmartContract smartContract = db.SmartContracts.Find(id);
            if (smartContract == null)
            {
                return HttpNotFound();
            }
            ViewBag.BlockChainAccountId = new SelectList(db.BlockchainAccounts, "BlockChainAccountId", "AccountName", smartContract.BlockChainAccountId);
            return View(smartContract);
        }

        // POST: SmartContracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SmartContractId,SmartContractName,SmartContractByteCode,SmartContractAbi,SmartContractAddress,SmartContractCode,SmartContractActive,BlockChainAccountId")] SmartContract smartContract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smartContract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BlockChainAccountId = new SelectList(db.BlockchainAccounts, "BlockChainAccountId", "AccountName", smartContract.BlockChainAccountId);
            return View(smartContract);
        }

        // GET: SmartContracts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SmartContract smartContract = db.SmartContracts.Find(id);
            if (smartContract == null)
            {
                return HttpNotFound();
            }
            return View(smartContract);
        }

        // POST: SmartContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SmartContract smartContract = db.SmartContracts.Find(id);
            db.SmartContracts.Remove(smartContract);
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
