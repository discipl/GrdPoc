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
    public class ProductCodeCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductCodeCategories
        public ActionResult Index()
        {
            return View(db.ProductCodeCategories.ToList());
        }

        // GET: ProductCodeCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCodeCategory productCodeCategory = db.ProductCodeCategories.Find(id);
            if (productCodeCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCodeCategory);
        }

        // GET: ProductCodeCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCodeCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductCodeCategoryId,ProductCodeCategoryNumber,ProductCodeCategoryDescription")] ProductCodeCategory productCodeCategory)
        {
            if (ModelState.IsValid)
            {
                db.ProductCodeCategories.Add(productCodeCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productCodeCategory);
        }

        // GET: ProductCodeCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCodeCategory productCodeCategory = db.ProductCodeCategories.Find(id);
            if (productCodeCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCodeCategory);
        }

        // POST: ProductCodeCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductCodeCategoryId,ProductCodeCategoryNumber,ProductCodeCategoryDescription")] ProductCodeCategory productCodeCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCodeCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productCodeCategory);
        }

        // GET: ProductCodeCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCodeCategory productCodeCategory = db.ProductCodeCategories.Find(id);
            if (productCodeCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCodeCategory);
        }

        // POST: ProductCodeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCodeCategory productCodeCategory = db.ProductCodeCategories.Find(id);
            db.ProductCodeCategories.Remove(productCodeCategory);
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
