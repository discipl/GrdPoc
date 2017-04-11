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
    public class IncidentalContractItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: IncidentalContractItems
        public ActionResult Index()
        {
            var incidentalContractItems = db.IncidentalContractItems.Include(i => i.IncidentalContract).Include(i => i.OwnerBudget).Include(i => i.ProductCodeExpense).Include(i => i.ProductCodeIncome);
            return View(incidentalContractItems.ToList());
        }

        // GET: IncidentalContractItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContractItem incidentalContractItem = db.IncidentalContractItems.Find(id);
            if (incidentalContractItem == null)
            {
                return HttpNotFound();
            }
            return View(incidentalContractItem);
        }

        // GET: IncidentalContractItems/Create
        public ActionResult Create()
        {
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle");
            ViewBag.OwnerBudgetId = new SelectList(db.Budgets, "BudgetId", "BudgetName");
            ViewBag.ProductExpenseCodeId = new SelectList(db.ProductCodes, "ProductCodeId", "ProductCodeNumber");
            ViewBag.ProductIncomeCodeId = new SelectList(db.ProductCodes, "ProductCodeId", "ProductCodeNumber");
            return View();
        }

        // POST: IncidentalContractItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IncidentalContractItemId,IncidentalContractItemDescription,IncidentalContractItemValue,IncidentalContractItemQuantity,IncidentalContractItemVat,ProductIncomeCode,ProductIncomeCodeId,ProductExpenseCode,ProductExpenseCodeId,BudgetCodeId,OwnerBudgetId,IncidentalContractId")] IncidentalContractItem incidentalContractItem)
        {
            if (ModelState.IsValid)
            {
                db.IncidentalContractItems.Add(incidentalContractItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", incidentalContractItem.IncidentalContractId);
            ViewBag.OwnerBudgetId = new SelectList(db.Budgets, "BudgetId", "BudgetName", incidentalContractItem.OwnerBudgetId);
            ViewBag.ProductExpenseCodeId = new SelectList(db.ProductCodes, "ProductCodeId", "ProductCodeNumber", incidentalContractItem.ProductExpenseCodeId);
            ViewBag.ProductIncomeCodeId = new SelectList(db.ProductCodes, "ProductCodeId", "ProductCodeNumber", incidentalContractItem.ProductIncomeCodeId);
            return View(incidentalContractItem);
        }

        // GET: IncidentalContractItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContractItem incidentalContractItem = db.IncidentalContractItems.Find(id);
            if (incidentalContractItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", incidentalContractItem.IncidentalContractId);
            ViewBag.OwnerBudgetId = new SelectList(db.Budgets, "BudgetId", "BudgetName", incidentalContractItem.OwnerBudgetId);
            ViewBag.ProductExpenseCodeId = new SelectList(db.ProductCodes, "ProductCodeId", "ProductCodeNumber", incidentalContractItem.ProductExpenseCodeId);
            ViewBag.ProductIncomeCodeId = new SelectList(db.ProductCodes, "ProductCodeId", "ProductCodeNumber", incidentalContractItem.ProductIncomeCodeId);
            return View(incidentalContractItem);
        }

        // POST: IncidentalContractItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IncidentalContractItemId,IncidentalContractItemDescription,IncidentalContractItemValue,IncidentalContractItemQuantity,IncidentalContractItemVat,ProductIncomeCode,ProductIncomeCodeId,ProductExpenseCode,ProductExpenseCodeId,BudgetCodeId,OwnerBudgetId,IncidentalContractId")] IncidentalContractItem incidentalContractItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidentalContractItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", incidentalContractItem.IncidentalContractId);
            ViewBag.OwnerBudgetId = new SelectList(db.Budgets, "BudgetId", "BudgetName", incidentalContractItem.OwnerBudgetId);
            ViewBag.ProductExpenseCodeId = new SelectList(db.ProductCodes, "ProductCodeId", "ProductCodeNumber", incidentalContractItem.ProductExpenseCodeId);
            ViewBag.ProductIncomeCodeId = new SelectList(db.ProductCodes, "ProductCodeId", "ProductCodeNumber", incidentalContractItem.ProductIncomeCodeId);
            return View(incidentalContractItem);
        }

        // GET: IncidentalContractItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContractItem incidentalContractItem = db.IncidentalContractItems.Find(id);
            if (incidentalContractItem == null)
            {
                return HttpNotFound();
            }
            return View(incidentalContractItem);
        }

        // POST: IncidentalContractItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidentalContractItem incidentalContractItem = db.IncidentalContractItems.Find(id);
            db.IncidentalContractItems.Remove(incidentalContractItem);
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
