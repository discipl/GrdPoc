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
    public class MunicipalEntitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MunicipalEntities
        public ActionResult Index()
        {
            return View(db.MunicipalEntities.ToList());
        }

        // GET: MunicipalEntities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MunicipalEntity municipalEntity = db.MunicipalEntities.Find(id);
            if (municipalEntity == null)
            {
                return HttpNotFound();
            }
            return View(municipalEntity);
        }

        // GET: MunicipalEntities/Create
        public ActionResult Create()
        {
            ViewBag.MunicipalEntityType = new SelectList(db.MunicipalEntityTypes, "MunicipalEntityTypeId", "MunicipalEntityName");

            return View();
        }

        // POST: MunicipalEntities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MunicipalEntityId,MunicipalEntityName,MunicipalEntityAddress,MunicipalEntityKvk,MunicipalEntityBtw,MunicipalEntityIban,MunicipalEntityType")] MunicipalEntity municipalEntity)
        {
            if (ModelState.IsValid)
            {
                db.MunicipalEntities.Add(municipalEntity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MunicipalEntityType = new SelectList(db.MunicipalEntityTypes, "MunicipalEntityTypeId", "MunicipalEntityName", municipalEntity.MunicipalEntityType);
            return View(municipalEntity);
        }

        // GET: MunicipalEntities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MunicipalEntity municipalEntity = db.MunicipalEntities.Find(id);
            if (municipalEntity == null)
            {
                return HttpNotFound();
            }
            return View(municipalEntity);
        }

        // POST: MunicipalEntities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MunicipalEntityId,MunicipalEntityName,MunicipalEntityAddress,MunicipalEntityKvk,MunicipalEntityBtw,MunicipalEntityIban,MunicipalEntityType")] MunicipalEntity municipalEntity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(municipalEntity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(municipalEntity);
        }

        // GET: MunicipalEntities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MunicipalEntity municipalEntity = db.MunicipalEntities.Find(id);
            if (municipalEntity == null)
            {
                return HttpNotFound();
            }
            return View(municipalEntity);
        }

        // POST: MunicipalEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MunicipalEntity municipalEntity = db.MunicipalEntities.Find(id);
            db.MunicipalEntities.Remove(municipalEntity);
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
