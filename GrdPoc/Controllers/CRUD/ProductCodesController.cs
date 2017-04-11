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
    public class ProductCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductCodes
        public ActionResult Index()
        {
            var productCodes = db.ProductCodes.Include(p => p.ProductCodeCategory);
            return View(productCodes.ToList());
        }

        // GET: ProductCodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCode productCode = db.ProductCodes.Find(id);
            if (productCode == null)
            {
                return HttpNotFound();
            }
            return View(productCode);
        }

        // GET: ProductCodes/Create
        public ActionResult Create()
        {
            ViewBag.ProductCodeCategoryId = new SelectList(db.ProductCodeCategories, "ProductCodeCategoryId", "ProductCodeCategoryDescription");
            return View();
        }

        // POST: ProductCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCodeId,ProductCodeNumber,ProductCodeType,ProductCodeDescription,ProductCodeCategoryId")] ProductCode productCode)
        {
            if (ModelState.IsValid)
            {
                db.ProductCodes.Add(productCode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductCodeCategoryId = new SelectList(db.ProductCodeCategories, "ProductCodeCategoryId", "ProductCodeCategoryDescription", productCode.ProductCodeCategoryId);
            return View(productCode);
        }

        // GET: ProductCodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCode productCode = db.ProductCodes.Find(id);
            if (productCode == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCodeCategoryId = new SelectList(db.ProductCodeCategories, "ProductCodeCategoryId", "ProductCodeCategoryDescription", productCode.ProductCodeCategoryId);
            return View(productCode);
        }

        // POST: ProductCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCodeId,ProductCodeNumber,ProductCodeType,ProductCodeDescription,ProductCodeCategoryId")] ProductCode productCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCodeCategoryId = new SelectList(db.ProductCodeCategories, "ProductCodeCategoryId", "ProductCodeCategoryDescription", productCode.ProductCodeCategoryId);
            return View(productCode);
        }

        // GET: ProductCodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCode productCode = db.ProductCodes.Find(id);
            if (productCode == null)
            {
                return HttpNotFound();
            }
            return View(productCode);
        }

        // POST: ProductCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCode productCode = db.ProductCodes.Find(id);
            db.ProductCodes.Remove(productCode);
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
