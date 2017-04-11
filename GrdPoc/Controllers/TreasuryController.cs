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
using GrdPoc.Models.ViewModels;
using AutoMapper;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class TreasuryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Treasury
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Invoices()
        {
            return RedirectToAction("List");
        }


        // GET: Invoices
        public ActionResult List()
        {
            return View(db.Invoices.ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);

            invoice.InvoiceItems = db.InvoiceItems.Where(w => w.InvoiceId == id).ToList();

            IncidentalContract contract = db.IncidentalContracts.Where(w => w.IncidentalContracInvoiceNumber == invoice.InvoiceNumber).FirstOrDefault();

            ViewBag.IncidentalContracId = contract.IncidentalContractId;

            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceId,InvoiceDescription,InvoiceIssuerName,InvoiceIssuerAddress,InvoiceIssuerKvk,InvoiceIssuerBtw,InvoiceIssuerIban,InvoiceDate,InvoiceDueDate,InvoiceCustomerName,InvoiceCustomeAddress,InvoiceCustomeKvk,InvoiceCustomeBtw,InvoiceValue,InvoiceVatTotal,InvoiceValueTotal")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceId,InvoiceDescription,InvoiceIssuerName,InvoiceIssuerAddress,InvoiceIssuerKvk,InvoiceIssuerBtw,InvoiceIssuerIban,InvoiceDate,InvoiceDueDate,InvoiceCustomerName,InvoiceCustomeAddress,InvoiceCustomeKvk,InvoiceCustomeBtw,InvoiceValue,InvoiceVatTotal,InvoiceValueTotal")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("List");
        }



        public ActionResult ViewService(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);
            ServiceIncidentalContract serviceIncidentalContract = db.ServiceIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
            List<IncidentalContractItem> incidentalContractItems = db.IncidentalContractItems.Where(w => w.IncidentalContractId == id).ToList();

            ServiceContractViewModel serviceContractViewModel = Mapper.Map<ServiceContractViewModel>(incidentalContract);
            serviceContractViewModel = Mapper.Map<ServiceContractViewModel>(serviceIncidentalContract);

            serviceContractViewModel.ContractTitle = incidentalContract.ContractTitle;
            serviceContractViewModel.ContractItems = incidentalContractItems;


            if (serviceContractViewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", serviceContractViewModel.IncidentalContracOwnerId);
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", serviceContractViewModel.IncidentalContracControllerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", serviceContractViewModel.IncidentalContracProviderId);
            ViewBag.IncidentalContracTypeId = new SelectList(db.IncidentalContracTypes, "IncidentalContracTypeId", "IncidentalContracTypeTitle", serviceContractViewModel.IncidentalContracTypeId);
            ViewBag.OwnerMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", serviceContractViewModel.OwnerMunicipalEntityId);
            ViewBag.ProviderMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", serviceContractViewModel.ProviderMunicipalEntityId);
            return View(serviceContractViewModel);
        }

        public ActionResult ViewProject(int? id)
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