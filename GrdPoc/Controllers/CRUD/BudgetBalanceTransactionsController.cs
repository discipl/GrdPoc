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
    public class BudgetBalanceTransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BudgetBalanceTransactions
        public ActionResult Index()
        {
            return View(db.BudgetBalanceTransactions.ToList());
        }

        // GET: BudgetBalanceTransactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetBalanceTransaction budgetBalanceTransaction = db.BudgetBalanceTransactions.Find(id);
            if (budgetBalanceTransaction == null)
            {
                return HttpNotFound();
            }
            return View(budgetBalanceTransaction);
        }

        // GET: BudgetBalanceTransactions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BudgetBalanceTransactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetBalanceTransactionId,BudgetBalanceDeposit,BudgetBalanceDepositSource,BudgetBalanceWithdrawal,BudgetBalanceWithdrawalSource")] BudgetBalanceTransaction budgetBalanceTransaction)
        {
            if (ModelState.IsValid)
            {
                db.BudgetBalanceTransactions.Add(budgetBalanceTransaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(budgetBalanceTransaction);
        }

        // GET: BudgetBalanceTransactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetBalanceTransaction budgetBalanceTransaction = db.BudgetBalanceTransactions.Find(id);
            if (budgetBalanceTransaction == null)
            {
                return HttpNotFound();
            }
            return View(budgetBalanceTransaction);
        }

        // POST: BudgetBalanceTransactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetBalanceTransactionId,BudgetBalanceDeposit,BudgetBalanceDepositSource,BudgetBalanceWithdrawal,BudgetBalanceWithdrawalSource")] BudgetBalanceTransaction budgetBalanceTransaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budgetBalanceTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(budgetBalanceTransaction);
        }

        // GET: BudgetBalanceTransactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetBalanceTransaction budgetBalanceTransaction = db.BudgetBalanceTransactions.Find(id);
            if (budgetBalanceTransaction == null)
            {
                return HttpNotFound();
            }
            return View(budgetBalanceTransaction);
        }

        // POST: BudgetBalanceTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BudgetBalanceTransaction budgetBalanceTransaction = db.BudgetBalanceTransactions.Find(id);
            db.BudgetBalanceTransactions.Remove(budgetBalanceTransaction);
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
