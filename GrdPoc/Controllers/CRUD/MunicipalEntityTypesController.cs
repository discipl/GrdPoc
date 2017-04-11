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
    public class MunicipalEntityTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MunicipalEntityTypes
        public ActionResult Index()
        {
            return View(db.MunicipalEntityTypes.ToList());
        }

        // GET: MunicipalEntityTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MunicipalEntityType municipalEntityType = db.MunicipalEntityTypes.Find(id);
            if (municipalEntityType == null)
            {
                return HttpNotFound();
            }
            return View(municipalEntityType);
        }

        // GET: MunicipalEntityTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MunicipalEntityTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MunicipalEntityTypeId,MunicipalEntityName")] MunicipalEntityType municipalEntityType)
        {
            if (ModelState.IsValid)
            {
                db.MunicipalEntityTypes.Add(municipalEntityType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(municipalEntityType);
        }

        // GET: MunicipalEntityTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MunicipalEntityType municipalEntityType = db.MunicipalEntityTypes.Find(id);
            if (municipalEntityType == null)
            {
                return HttpNotFound();
            }
            return View(municipalEntityType);
        }

        // POST: MunicipalEntityTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MunicipalEntityTypeId,MunicipalEntityName")] MunicipalEntityType municipalEntityType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipalEntityType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(municipalEntityType);
        }

        // GET: MunicipalEntityTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MunicipalEntityType municipalEntityType = db.MunicipalEntityTypes.Find(id);
            if (municipalEntityType == null)
            {
                return HttpNotFound();
            }
            return View(municipalEntityType);
        }

        // POST: MunicipalEntityTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MunicipalEntityType municipalEntityType = db.MunicipalEntityTypes.Find(id);
            db.MunicipalEntityTypes.Remove(municipalEntityType);
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
