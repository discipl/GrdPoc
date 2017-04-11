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
    public class InvoiceItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InvoiceItems
        public ActionResult Index()
        {
            var invoiceItems = db.InvoiceItems.Include(i => i.Invoice);
            return View(invoiceItems.ToList());
        }

        // GET: InvoiceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceId = new SelectList(db.Invoices, "InvoiceId", "InvoiceDescription");
            return View();
        }

        // POST: InvoiceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceItemId,InvoiceId,Description,UnitPrice,Quantity,VatPercent,TotalPrice,Currency")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceItems.Add(invoiceItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceId = new SelectList(db.Invoices, "InvoiceId", "InvoiceDescription", invoiceItem.InvoiceId);
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "InvoiceId", "InvoiceDescription", invoiceItem.InvoiceId);
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceItemId,InvoiceId,Description,UnitPrice,Quantity,VatPercent,TotalPrice,Currency")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceId = new SelectList(db.Invoices, "InvoiceId", "InvoiceDescription", invoiceItem.InvoiceId);
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            db.InvoiceItems.Remove(invoiceItem);
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
