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
    public class PersonaTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PersonaTypes
        public ActionResult Index()
        {
            return View(db.PersonaTypes.ToList());
        }

        // GET: PersonaTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonaType personaType = db.PersonaTypes.Find(id);
            if (personaType == null)
            {
                return HttpNotFound();
            }
            return View(personaType);
        }

        // GET: PersonaTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonaTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonaTypeId,PersonaTypeName")] PersonaType personaType)
        {
            if (ModelState.IsValid)
            {
                db.PersonaTypes.Add(personaType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personaType);
        }

        // GET: PersonaTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonaType personaType = db.PersonaTypes.Find(id);
            if (personaType == null)
            {
                return HttpNotFound();
            }
            return View(personaType);
        }

        // POST: PersonaTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonaTypeId,PersonaTypeName")] PersonaType personaType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personaType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personaType);
        }

        // GET: PersonaTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonaType personaType = db.PersonaTypes.Find(id);
            if (personaType == null)
            {
                return HttpNotFound();
            }
            return View(personaType);
        }

        // POST: PersonaTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonaType personaType = db.PersonaTypes.Find(id);
            db.PersonaTypes.Remove(personaType);
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
