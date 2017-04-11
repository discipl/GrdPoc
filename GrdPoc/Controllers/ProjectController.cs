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
using AutoMapper;
using GrdPoc.Models.ViewModels;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private int UserAccountId
        {
            get
            {
                return (int?)Session["currentUserId"] ?? 0;
            }
        }
        private string UserName
        {
            get
            {
                return (CurrentUser?.UserName ?? "");
            }
        }

        private UserAccount currentUser;
        private UserAccount CurrentUser
        {
            get
            {
                int userId = (int?)Session["currentUserId"] ?? 0;
                if (currentUser == null)
                {
                    currentUser = db.UserAccounts.Find(userId);
                }

                return (currentUser ?? new UserAccount());
            }
        }

        private string Persona
        {
            get
            {
                return Session["Persona"]?.ToString();
            }
        }


        // GET: ExecutionProjects
        public ActionResult Index()
        {
            var executionProjects = db.ExecutionProjects.Include(e => e.ExecutionProjectController).Include(e => e.ExecutionProjectProvider).Include(e => e.ExecutionProjectResource).Include(e => e.IncidentalContract);
            return View(executionProjects.ToList());
        }

        public ActionResult List()
        {
            if (Persona == "Team Leader")
            {
                return RedirectToAction("ListConfigure");
            }
            if (Persona == "Project Leader")
            {
                return RedirectToAction("ListDelivered");
            }
            if (Persona == "Project Resource")
            {
                return RedirectToAction("ListDelivered");
            }
            return RedirectToAction("ListClosed");
        }


        public ActionResult ListConfigure()
        {
            var executionProjects = db.ExecutionProjects.Where(w => w.ExecutionProjectStatus == ProjectStatus.Unconfigured
                                                                 && w.ExecutionProjectProviderId == UserAccountId)
                                                        .Include(e => e.ExecutionProjectController)
                                                        .Include(e => e.ExecutionProjectProvider)
                                                        .Include(e => e.ExecutionProjectResource)
                                                        .Include(e => e.IncidentalContract);
            return View(executionProjects.ToList());
        }

        public ActionResult ListToStart()
        {
            var executionProjects = db.ExecutionProjects.Where(w => w.ExecutionProjectStatus == ProjectStatus.Configured
                                                                 && w.ExecutionProjectProviderId == UserAccountId)
                                                        .Include(e => e.ExecutionProjectController)
                                                        .Include(e => e.ExecutionProjectProvider)
                                                        .Include(e => e.ExecutionProjectResource)
                                                        .Include(e => e.IncidentalContract);
            return View(executionProjects.ToList());
        }

        public ActionResult ListExecution()
        {
            var executionProjects = db.ExecutionProjects.Where(w => w.ExecutionProjectStatus == ProjectStatus.Executing
                                                                 && w.ExecutionProjectResourceId == UserAccountId)
                                                        .Include(e => e.ExecutionProjectController)
                                                        .Include(e => e.ExecutionProjectProvider)
                                                        .Include(e => e.ExecutionProjectResource)
                                                        .Include(e => e.IncidentalContract);
            return View(executionProjects.ToList());
        }

        public ActionResult ListDelivered()
        {
            var executionProjects = db.ExecutionProjects.Where(w => w.ExecutionProjectStatus == ProjectStatus.Delivered
                                                                 && (w.ExecutionProjectControllerId == UserAccountId))
                                                        .Include(e => e.ExecutionProjectController)
                                                        .Include(e => e.ExecutionProjectProvider)
                                                        .Include(e => e.ExecutionProjectResource)
                                                        .Include(e => e.IncidentalContract);
            return View(executionProjects.ToList());
        }

        public ActionResult ListClosed()
        {
            var executionProjects = db.ExecutionProjects.Where(w => (w.ExecutionProjectStatus == ProjectStatus.Confirmed 
                                                                  || w.ExecutionProjectStatus == ProjectStatus.Faulty)
                                                                 && (w.ExecutionProjectProviderId == UserAccountId 
                                                                 || w.ExecutionProjectControllerId == UserAccountId 
                                                                 || w.ExecutionProjectResourceId == UserAccountId))
                                                        .Include(e => e.ExecutionProjectController)
                                                        .Include(e => e.ExecutionProjectProvider)
                                                        .Include(e => e.ExecutionProjectResource)
                                                        .Include(e => e.IncidentalContract);
            return View(executionProjects.ToList());
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
            ViewBag.ExecutionProjectControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", executionProject.ExecutionProjectControllerId);
            ViewBag.ExecutionProjectProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", executionProject.ExecutionProjectProviderId);
            ViewBag.ExecutionProjectResourceId = new SelectList(db.UserAccounts.Where(w => w.PersonaTypeId == 4), "UserAccountId", "UserName", executionProject.ExecutionProjectResourceId);
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
                var project = db.ExecutionProjects.Find(executionProject.ExecutionProjectId);

                switch (executionProject.ExecutionProjectStatus)
                {
                    case ProjectStatus.Unconfigured:
                        project.ExecutionProjectTitle = executionProject.ExecutionProjectTitle;
                        project.ExecutionProjectResourceId = executionProject.ExecutionProjectResourceId;
                        project.ExecutionProjectSchedulledStart = executionProject.ExecutionProjectSchedulledStart;
                        project.ExecutionProjectSchedulledEnd = executionProject.ExecutionProjectSchedulledEnd;
                        project.ExecutionProjectStatus = ProjectStatus.Configured;
                        break;
                    case ProjectStatus.Configured:
                        project.ExecutionProjectActualStart = executionProject.ExecutionProjectActualStart;
                        project.ExecutionProjectStatus = ProjectStatus.Executing;
                        break;
                    case ProjectStatus.Executing:
                        project.ExecutionProjectActualEnd = executionProject.ExecutionProjectActualEnd;
                        project.ExecutionProjectDeliveranceDate = executionProject.ExecutionProjectActualEnd;
                        project.ExecutionProjectStatus = ProjectStatus.Delivered;

                        //deliver contract
                        DeliverContract(executionProject.IncidentalContracId);
                        //invoice payment (option 1)
                        //CreateInvoice(executionProject.IncidentalContracId);
                        break;
                    case ProjectStatus.Delivered:
                        project.ExecutionProjectDeliveranceConfirmation = executionProject.ExecutionProjectDeliveranceConfirmation;
                        project.ExecutionProjectStatus = ProjectStatus.Confirmed;

                        //confirm contract
                        ConfirmContract(executionProject.IncidentalContracId);
                        //invoice payment (option 2)
                        CreateInvoice(executionProject.IncidentalContracId);
                        break;
                }


                //db.Entry(executionProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.ExecutionProjectControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", executionProject.ExecutionProjectControllerId);
            ViewBag.ExecutionProjectProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", executionProject.ExecutionProjectProviderId);
            ViewBag.ExecutionProjectResourceId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", executionProject.ExecutionProjectResourceId);
            ViewBag.IncidentalContracId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", executionProject.IncidentalContracId);
            return View(executionProject);
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


        private void DeliverContract(int? id)
        {

            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);

            if (incidentalContract == null)
            {
                return;
            }

            incidentalContract.IncidentalContracStatus = ContractStatus.Delivered;

            switch (incidentalContract.IncidentalContracTypeId)
            {
                case 1:
                    var servicecontract = db.ServiceIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    servicecontract.IncidentalContracStatus = ContractStatus.Delivered;
                    servicecontract.ProviderDeliverenceConfirmationDate = DateTime.Now.Date;
                    break;
                case 2:
                    var productcontract = db.ProductIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    productcontract.IncidentalContracStatus = ContractStatus.Delivered;
                    productcontract.ProviderDeliverenceConfirmationDate = DateTime.Now.Date;
                    break;
                case 3:
                    var trainningcontract = db.TrainningIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    trainningcontract.IncidentalContracStatus = ContractStatus.Delivered;
                    trainningcontract.ProviderDeliverenceConfirmationDate = DateTime.Now.Date;
                    break;
            }

            db.SaveChanges();
        }

        private void ConfirmContract(int? id)
        {

            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);

            if (incidentalContract == null)
            {
                return;
            }

            incidentalContract.IncidentalContracStatus = ContractStatus.Confirmed;

            switch (incidentalContract.IncidentalContracTypeId)
            {
                case 1:
                    var servicecontract = db.ServiceIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    servicecontract.IncidentalContracStatus = ContractStatus.Confirmed;
                    servicecontract.ControllerDeliverenceConfirmationDate = DateTime.Now.Date;
                    break;
                case 2:
                    var productcontract = db.ProductIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    productcontract.IncidentalContracStatus = ContractStatus.Confirmed;
                    productcontract.ControllerDeliverenceConfirmationDate = DateTime.Now.Date;
                    break;
                case 3:
                    var trainningcontract = db.TrainningIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    trainningcontract.IncidentalContracStatus = ContractStatus.Confirmed;
                    trainningcontract.ControllerDeliverenceConfirmationDate = DateTime.Now.Date;
                    break;
            }

            db.SaveChanges();
        }

        private void CreateInvoice(int? id)
        {
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);

            if (incidentalContract == null)
            {
                return;
            }

            var invoice = new Invoice();

            var issuer = db.UserAccounts.Find(incidentalContract.IncidentalContracProviderId).MunicipalEntity;
            var customer = db.UserAccounts.Find(incidentalContract.IncidentalContracOwnerId).MunicipalEntity;

            invoice.InvoiceIssuerName = issuer.MunicipalEntityName;
            invoice.InvoiceIssuerAddress = issuer.MunicipalEntityAddress;
            invoice.InvoiceIssuerBtw = issuer.MunicipalEntityBtw;
            invoice.InvoiceIssuerKvk = issuer.MunicipalEntityKvk;
            invoice.InvoiceIssuerIban = issuer.MunicipalEntityIban;

            invoice.InvoiceCustomerName = customer.MunicipalEntityName;
            invoice.InvoiceCustomeAddress = customer.MunicipalEntityAddress;
            invoice.InvoiceCustomeBtw = customer.MunicipalEntityBtw;
            invoice.InvoiceCustomeKvk = customer.MunicipalEntityKvk;

            invoice.InvoiceDescription = "";
            invoice.InvoiceDate = DateTime.Now.Date;
            invoice.InvoiceDueDate = DateTime.Now.AddMonths(1).Date;

            decimal InvoiceValueSum = 0M;
            decimal InvoiceVatSum = 0M;

            

            foreach (var item in db.IncidentalContractItems.Where(w => w.IncidentalContractId == id).ToList())
            {
                var invoiceItem = new InvoiceItem();

                invoiceItem.Currency = "EUR";
                invoiceItem.Description = item.IncidentalContractItemDescription;
                invoiceItem.Quantity = item.IncidentalContractItemQuantity;
                invoiceItem.UnitPrice = item.IncidentalContractItemValue;
                invoiceItem.VatPercent = item.IncidentalContractItemVat;
                invoiceItem.TotalPrice = item.IncidentalContractItemQuantity * item.IncidentalContractItemValue;
                invoiceItem.Description = item.IncidentalContractItemDescription;

                invoiceItem.ProductIncomeCode = item.ProductIncomeCode;
                invoiceItem.ProductExpenseCode = item.ProductExpenseCode;
                invoiceItem.BudgetCode = item.BudgetCodeId;

                InvoiceValueSum += invoiceItem.TotalPrice;
                InvoiceVatSum += (invoiceItem.TotalPrice * ((decimal)invoiceItem.VatPercent / 100));

                invoice.InvoiceItems.Add(invoiceItem);
            }

            invoice.InvoiceValue = InvoiceValueSum;
            invoice.InvoiceVatTotal = InvoiceVatSum;
            invoice.InvoiceValueTotal = InvoiceValueSum + InvoiceVatSum;

            //here call rest API, for this exercise we will write it to the database directly
            db.Invoices.Add(invoice);
            db.SaveChanges();

            foreach (var item in db.IncidentalContractItems.Where(w => w.IncidentalContractId == id).ToList())
            {
                var budget = db.Budgets.Where(w => w.BudgetIdCode == item.BudgetCodeId).FirstOrDefault();
                var transaction = new BudgetBalanceTransaction();

                transaction.BudgetBalanceWithdrawal = item.IncidentalContractItemQuantity * item.IncidentalContractItemValue; 
                transaction.BudgetBalanceWithdrawalSource = invoice.InvoiceNumber;
                transaction.OwnerBudgetId = budget.BudgetId;

                db.BudgetBalanceTransactions.Add(transaction);

                budget.BudgetBalance -= transaction.BudgetBalanceWithdrawal;
            }


            incidentalContract.IncidentalContracInvoiceNumber = invoice.InvoiceNumber;
            db.SaveChanges();
        }
    }
}
