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
    public class ExecutionProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ExecutionProjects
        public ActionResult Index()
        {
            var executionProjects = db.ExecutionProjects.Include(e => e.ExecutionProjectController).Include(e => e.ExecutionProjectProvider).Include(e => e.ExecutionProjectResource).Include(e => e.IncidentalContract);
            return View(executionProjects.ToList());
        }

        // GET: ExecutionProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExecutionProject executionProject = db.ExecutionProjects.Find(id);
            if (executionProject == null)
            {
                return HttpNotFound();
            }
            return View(executionProject);
        }

        // GET: ExecutionProjects/Create
        public ActionResult Create()
        {
            ViewBag.ExecutionProjectControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.ExecutionProjectProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.ExecutionProjectResourceId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle");
            return View();
        }

        // POST: ExecutionProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExecutionProjectId,ExecutionProjectTitle,IncidentalContracId,ExecutionProjectStatus,ExecutionProjectControllerId,ExecutionProjectProviderId,ExecutionProjectResourceId,ExecutionProjectDuration,ExecutionProjectTimeframeStart,ExecutionProjectTimeframeEnd,ExecutionProjectSchedulledStart,ExecutionProjectSchedulledEnd,ExecutionProjectActualStart,ExecutionProjectActualEnd,ExecutionProjectDeliveranceDate,ExecutionProjectDeliveranceConfirmation")] ExecutionProject executionProject)
        {
            if (ModelState.IsValid)
            {
                db.ExecutionProjects.Add(executionProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExecutionProjectControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", executionProject.ExecutionProjectControllerId);
            ViewBag.ExecutionProjectProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", executionProject.ExecutionProjectProviderId);
            ViewBag.ExecutionProjectResourceId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", executionProject.ExecutionProjectResourceId);
            ViewBag.IncidentalContracId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", executionProject.IncidentalContracId);
            return View(executionProject);
        }

        // GET: ExecutionProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExecutionProject executionProject = db.ExecutionProjects.Find(id);
            if (executionProject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExecutionProjectControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", executionProject.ExecutionProjectControllerId);
            ViewBag.ExecutionProjectProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", executionProject.ExecutionProjectProviderId);
            ViewBag.ExecutionProjectResourceId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", executionProject.ExecutionProjectResourceId);
            ViewBag.IncidentalContracId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", executionProject.IncidentalContracId);
            return View(executionProject);
        }

        // POST: ExecutionProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExecutionProjectId,ExecutionProjectTitle,IncidentalContracId,ExecutionProjectStatus,ExecutionProjectControllerId,ExecutionProjectProviderId,ExecutionProjectResourceId,ExecutionProjectDuration,ExecutionProjectTimeframeStart,ExecutionProjectTimeframeEnd,ExecutionProjectSchedulledStart,ExecutionProjectSchedulledEnd,ExecutionProjectActualStart,ExecutionProjectActualEnd,ExecutionProjectDeliveranceDate,ExecutionProjectDeliveranceConfirmation")] ExecutionProject executionProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(executionProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExecutionProjectControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", executionProject.ExecutionProjectControllerId);
            ViewBag.ExecutionProjectProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", executionProject.ExecutionProjectProviderId);
            ViewBag.ExecutionProjectResourceId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", executionProject.ExecutionProjectResourceId);
            ViewBag.IncidentalContracId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", executionProject.IncidentalContracId);
            return View(executionProject);
        }

        // GET: ExecutionProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExecutionProject executionProject = db.ExecutionProjects.Find(id);
            if (executionProject == null)
            {
                return HttpNotFound();
            }
            return View(executionProject);
        }

        // POST: ExecutionProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExecutionProject executionProject = db.ExecutionProjects.Find(id);
            db.ExecutionProjects.Remove(executionProject);
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
