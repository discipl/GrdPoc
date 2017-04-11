using GrdPoc.Models;
using GrdPoc.Models.Entities;
using GrdPoc.Models.ViewModels;
using Nelibur.ObjectMapper;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class ContractController : Controller
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

        // GET: Contract (dashboard)
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Conclude(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);

            if (incidentalContract == null)
            {
                return HttpNotFound();
            }

            incidentalContract.IncidentalContracStatus = ContractStatus.ApprovalPending;
            switch (incidentalContract.IncidentalContracTypeId)
            {
                case 1:
                    var servicecontract = db.ServiceIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    servicecontract.IncidentalContracStatus = ContractStatus.ApprovalPending;
                    if (incidentalContract.IncidentalContracOwnerId == UserAccountId)
                        servicecontract.OwnerTermsAgreementDate = DateTime.Now.Date;
                    if (incidentalContract.IncidentalContracProviderId == UserAccountId)
                        servicecontract.ProviderTermsAgreementDate = DateTime.Now.Date;
                    break;
                case 2:
                    var productcontract = db.ProductIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    productcontract.IncidentalContracStatus = ContractStatus.ApprovalPending;
                    if (incidentalContract.IncidentalContracOwnerId == UserAccountId)
                        productcontract.OwnerTermsAgreementDate = DateTime.Now.Date;
                    if (incidentalContract.IncidentalContracProviderId == UserAccountId)
                        productcontract.ProviderTermsAgreementDate = DateTime.Now.Date;
                    break;
                case 3:
                    var trainningcontract = db.TrainningIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    trainningcontract.IncidentalContracStatus = ContractStatus.ApprovalPending;
                    if (incidentalContract.IncidentalContracOwnerId == UserAccountId)
                        trainningcontract.OwnerTermsAgreementDate = DateTime.Now.Date;
                    if (incidentalContract.IncidentalContracProviderId == UserAccountId)
                        trainningcontract.ProviderTermsAgreementDate = DateTime.Now.Date;
                    break;
                default:
                    var servicecontractdefault = db.ServiceIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    servicecontractdefault.IncidentalContracStatus = ContractStatus.ApprovalPending;
                    if (incidentalContract.IncidentalContracOwnerId == UserAccountId)
                        servicecontractdefault.OwnerTermsAgreementDate = DateTime.Now.Date;
                    if (incidentalContract.IncidentalContracProviderId == UserAccountId)
                        servicecontractdefault.ProviderTermsAgreementDate = DateTime.Now.Date;
                    incidentalContract.IncidentalContracTypeId = 1;
                    servicecontractdefault.IncidentalContracTypeId = 1;
                    break;
            }

            db.SaveChanges();

            return RedirectToAction("List");
        }

        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);

            if (incidentalContract == null)
            {
                return HttpNotFound();
            }

            incidentalContract.IncidentalContracStatus = ContractStatus.Approved;

            var project = new ExecutionProject();
            project = Mapper.Map<ExecutionProject>(incidentalContract);

            project.ExecutionProjectTitle = incidentalContract.ContractTitle;
            project.ExecutionProjectControllerId = incidentalContract.IncidentalContracControllerId;
            project.ExecutionProjectProviderId = incidentalContract.IncidentalContracProviderId;
            project.IncidentalContracId = incidentalContract.IncidentalContractId;

            switch (incidentalContract.IncidentalContracTypeId)
            {
                case 1:
                    var servicecontract = db.ServiceIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    servicecontract.IncidentalContracStatus = ContractStatus.Approved;
                    if (incidentalContract.IncidentalContracOwnerId == UserAccountId)
                        servicecontract.OwnerTermsAgreementDate = DateTime.Now.Date;
                    if (incidentalContract.IncidentalContracProviderId == UserAccountId)
                        servicecontract.ProviderTermsAgreementDate = DateTime.Now.Date;
                    project.ExecutionProjectDuration = servicecontract.IncidentalContracDuration;
                    project.ExecutionProjectTimeframeEnd = servicecontract.IncidentalContracTimeframeEnd;
                    project.ExecutionProjectTimeframeStart = servicecontract.IncidentalContracTimeframeStart;
                    break;
                case 2:
                    var productcontract = db.ProductIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    productcontract.IncidentalContracStatus = ContractStatus.Approved;
                    if (incidentalContract.IncidentalContracOwnerId == UserAccountId)
                        productcontract.OwnerTermsAgreementDate = DateTime.Now.Date;
                    if (incidentalContract.IncidentalContracProviderId == UserAccountId)
                        productcontract.ProviderTermsAgreementDate = DateTime.Now.Date;
                    project.ExecutionProjectDuration = productcontract.IncidentalContracDuration;
                    project.ExecutionProjectTimeframeEnd = productcontract.IncidentalContracTimeframeEnd;
                    project.ExecutionProjectTimeframeStart = productcontract.IncidentalContracTimeframeStart;
                    break;
                case 3:
                    var trainningcontract = db.TrainningIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    trainningcontract.IncidentalContracStatus = ContractStatus.Approved;
                    if (incidentalContract.IncidentalContracOwnerId == UserAccountId)
                        trainningcontract.OwnerTermsAgreementDate = DateTime.Now.Date;
                    if (incidentalContract.IncidentalContracProviderId == UserAccountId)
                        trainningcontract.ProviderTermsAgreementDate = DateTime.Now.Date;
                    project.ExecutionProjectDuration = trainningcontract.IncidentalContracDuration;
                    project.ExecutionProjectTimeframeEnd = trainningcontract.IncidentalContracTimeframeEnd;
                    project.ExecutionProjectTimeframeStart = trainningcontract.IncidentalContracTimeframeStart;
                    break;
            }

            db.ExecutionProjects.Add(project);
            db.SaveChanges();

            return RedirectToAction("List");
        }


        public ActionResult BackToNegotiation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);

            if (incidentalContract == null)
            {
                return HttpNotFound();
            }

            incidentalContract.IncidentalContracStatus = ContractStatus.Initiated;
            switch (incidentalContract.IncidentalContracTypeId)
            {
                case 1:
                    var servicecontract = db.ServiceIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    servicecontract.IncidentalContracStatus = ContractStatus.Initiated;
                    servicecontract.OwnerTermsAgreementDate = null;
                    servicecontract.ProviderTermsAgreementDate = null;
                    break;
                case 2:
                    var productcontract = db.ProductIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    productcontract.IncidentalContracStatus = ContractStatus.Initiated;
                    productcontract.OwnerTermsAgreementDate = null;
                    productcontract.ProviderTermsAgreementDate = null;
                    break;
                case 3:
                    var trainningcontract = db.TrainningIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
                    trainningcontract.IncidentalContracStatus = ContractStatus.Initiated;
                    trainningcontract.OwnerTermsAgreementDate = null;
                    trainningcontract.ProviderTermsAgreementDate = null;
                    break;
            }

            db.SaveChanges();

            return RedirectToAction("List");
        }



        public ActionResult List()
        {
            if (Persona == "Budget Owner")
            {
                return RedirectToAction("ListOpen");
            }
            if (Persona == "Team Leader")
            {
                return RedirectToAction("ListOpen");
            }
            if (Persona == "Project Leader")
            {
                return RedirectToAction("ListController");
            }
            return RedirectToAction("ListOpen");
        }

        #region Contracts Listings

        public ActionResult ListOpen()
        {
            if (Persona == "Budget Owner")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Initiated
                                                                         && (w.IncidentalContracOwnerId == UserAccountId 
                                                                          || w.IncidentalContracProviderId == UserAccountId))
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            if (Persona == "Team Leader")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Initiated
                                                                         && w.IncidentalContracProviderId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            return View();
        }
        public ActionResult ListApprove()
        {
            if (Persona == "Budget Owner")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.ApprovalPending
                                                                         && (w.IncidentalContracOwnerId == UserAccountId 
                                                                          || w.IncidentalContracProviderId == UserAccountId))
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            if (Persona == "Team Leader")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.ApprovalPending
                                                                         && w.IncidentalContracProviderId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            return View();
        }
        public ActionResult ListOwner()
        {
            if (Persona == "Budget Owner")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Initiated 
                                                                         && w.IncidentalContracOwnerId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            return View();
        }
        public ActionResult ListProvider()
        {
            if (Persona == "Budget Owner")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Initiated 
                                                                         && w.IncidentalContracProviderId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            if (Persona == "Team Leader")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Initiated 
                                                                         && w.IncidentalContracProviderId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            return View();
        }
        public ActionResult ListController()
        {
            if (Persona == "Project Leader")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Initiated 
                                                                         && w.IncidentalContracControllerId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            return View();
        }
        public ActionResult ListDelivered()
        {
            if (Persona == "Budget Owner")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Delivered
                                                                         && w.IncidentalContracOwnerId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            if (Persona == "Team Leader")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Delivered
                                                                         && w.IncidentalContracProviderId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            if (Persona == "Project Leader")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Delivered
                                                                         && w.IncidentalContracControllerId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            return View();
        }
        public ActionResult ListClosed()
        {
            if (Persona == "Budget Owner")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Confirmed
                                                                         && w.IncidentalContracOwnerId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            if (Persona == "Team Leader")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Confirmed
                                                                         && w.IncidentalContracProviderId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            if (Persona == "Project Leader")
            {
                var incidentalContracts = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Confirmed
                                                                         && w.IncidentalContracControllerId == UserAccountId)
                                                                .Include(i => i.OwnerMunicipalEntity)
                                                                .Include(i => i.ProviderMunicipalEntity);
                return View(incidentalContracts.ToList());
            }
            return View();
        }

        #endregion

        // GET: Contract/View/5 (does the routing to the correct view)
        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);
            if (incidentalContract == null)
            {
                return HttpNotFound();
            }

            switch (incidentalContract.IncidentalContracTypeId)
            {
                case 1:
                    return RedirectToAction("ViewService", new { id = incidentalContract.IncidentalContractId });
                case 2:
                    return RedirectToAction("ViewProduct", new { id = incidentalContract.IncidentalContractId });
                case 3:
                    return RedirectToAction("ViewTrainning", new { id = incidentalContract.IncidentalContractId });
                default:
                    return RedirectToAction("ViewService", new { id = incidentalContract.IncidentalContractId });
            }
        }

        // GET: Contract/Edit/5 (does the routing to the correct view)
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);
            if (incidentalContract == null)
            {
                return HttpNotFound();
            }

            switch (incidentalContract.IncidentalContracTypeId)
            {
                case 1:
                    return RedirectToAction("EditService", new { id = incidentalContract.IncidentalContractId });
                case 2:
                    return RedirectToAction("EditProduct", new { id = incidentalContract.IncidentalContractId });
                case 3:
                    return RedirectToAction("EditTrainning", new { id = incidentalContract.IncidentalContractId });
                default:
                    return RedirectToAction("EditService", new { id = incidentalContract.IncidentalContractId });
            }
        }


        #region Service Contracts

        // GET:  Contracts/CreateService
        public ActionResult CreateService()
        {
            switch (CurrentUser.PersonaTypeId)
            {
                case 1:
                    ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts.Where(w => w.UserAccountId == UserAccountId), "UserAccountId", "UserName");
                    ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts.Where(w => w.MunicipalEntityId == CurrentUser.MunicipalEntityId && w.PersonaTypeId == 3), "UserAccountId", "UserName");
                    ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts.Where(w => w.PersonaTypeId == 2 ).OrderBy(o => o.UserName), "UserAccountId", "UserName");
                    ViewBag.IncidentalContracTypeId = new SelectList(db.IncidentalContracTypes, "IncidentalContracTypeId", "IncidentalContracTypeTitle");
                    ViewBag.OwnerMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName");
                    ViewBag.ProviderMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName");
                    break;

                case 2:
                    ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts.Where(w => w.PersonaTypeId == 1).OrderBy(o => o.UserName), "UserAccountId", "UserName");
                    ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts.Where(w => w.PersonaTypeId == 3).OrderBy(o => o.MunicipalEntityId), "UserAccountId", "UserName");
                    ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts.Where(w => w.UserAccountId == UserAccountId), "UserAccountId", "UserName");
                    ViewBag.IncidentalContracTypeId = new SelectList(db.IncidentalContracTypes, "IncidentalContracTypeId", "IncidentalContracTypeTitle");
                    ViewBag.OwnerMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName");
                    ViewBag.ProviderMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName");
                    break;

                default:
                    ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserName");
                    ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserName");
                    ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserName");
                    ViewBag.IncidentalContracTypeId = new SelectList(db.IncidentalContracTypes, "IncidentalContracTypeId", "IncidentalContracTypeTitle");
                    ViewBag.OwnerMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName");
                    ViewBag.ProviderMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName");
                    break;
            }

            ServiceContractViewModel serviceContractViewModel = new ServiceContractViewModel();
            serviceContractViewModel.ServiceIncidentalContractId = 0;

            return View();
        }

        // POST:  Contracts/CreateService
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateService(ServiceContractViewModel serviceContractViewModel)
        {
            if (ModelState.IsValid)
            {
                var serviceIncidentalContract = Mapper.Map<ServiceIncidentalContract>(serviceContractViewModel);
                var incidentalContract = Mapper.Map<IncidentalContract>(serviceContractViewModel);

                incidentalContract.IncidentalContracOwnerName = db.UserAccounts.Find(serviceIncidentalContract.IncidentalContracOwnerId).UserName;
                incidentalContract.IncidentalContracProviderName = db.UserAccounts.Find(serviceIncidentalContract.IncidentalContracProviderId).UserName;
                incidentalContract.IncidentalContracControllerName = db.UserAccounts.Find(serviceIncidentalContract.IncidentalContracControllerId).UserName;

                incidentalContract.OwnerMunicipalEntityId = db.UserAccounts.Find(serviceIncidentalContract.IncidentalContracOwnerId).MunicipalEntityId;

                //incidentalContract.IncidentalContracTypeId = serviceIncidentalContract.IncidentalContracTypeId;
                incidentalContract.IncidentalContracTypeId = db.IncidentalContracTypes.Where(w => w.IncidentalContracDescription.ToUpper() == "SERVICE").FirstOrDefault()?.IncidentalContracTypeId;

                incidentalContract.ServiceIncidentalContracts.Add(serviceIncidentalContract);
                db.IncidentalContracts.Add(incidentalContract);

                db.SaveChanges();
                return RedirectToAction("EditService", new { Id = incidentalContract.IncidentalContractId });
            }

            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", serviceContractViewModel.IncidentalContracOwnerId);
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", serviceContractViewModel.IncidentalContracControllerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", serviceContractViewModel.IncidentalContracProviderId);
            ViewBag.IncidentalContracTypeId = new SelectList(db.IncidentalContracTypes, "IncidentalContracTypeId", "IncidentalContracTypeTitle", serviceContractViewModel.IncidentalContracTypeId);
            ViewBag.OwnerMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", serviceContractViewModel.OwnerMunicipalEntityId);
            ViewBag.ProviderMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", serviceContractViewModel.ProviderMunicipalEntityId);
            return View(serviceContractViewModel);
        }

        // GET: Contracts/EditService/5
        public ActionResult ViewService(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);
            ServiceIncidentalContract serviceIncidentalContract =  db.ServiceIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
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

        // GET: Contracts/EditService/5
        public ActionResult EditService(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);
            ServiceIncidentalContract serviceIncidentalContract =  db.ServiceIncidentalContracts.Where(w => w.IncidentalContractId == id).FirstOrDefault();
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

        // POST: Contracts/EditService/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditService(ServiceContractViewModel serviceContractViewModel)
        {
            if (ModelState.IsValid)
            {
                var serviceIncidentalContract = Mapper.Map<ServiceIncidentalContract>(serviceContractViewModel);
                var incidentalContract = Mapper.Map<IncidentalContract>(serviceContractViewModel);

                //serviceIncidentalContract.IncidentalContracStatus = incidentalContract.IncidentalContracStatus;

                incidentalContract.IncidentalContracOwnerName = db.UserAccounts.Find(serviceIncidentalContract.IncidentalContracOwnerId).UserName;
                incidentalContract.IncidentalContracProviderName = db.UserAccounts.Find(serviceIncidentalContract.IncidentalContracProviderId).UserName;
                incidentalContract.IncidentalContracControllerName = db.UserAccounts.Find(serviceIncidentalContract.IncidentalContracControllerId).UserName;

                incidentalContract.OwnerMunicipalEntityId = db.UserAccounts.Find(serviceIncidentalContract.IncidentalContracOwnerId).MunicipalEntityId;

                if (serviceIncidentalContract.IncidentalContracTypeId == null)
                { 
                    serviceIncidentalContract.IncidentalContracTypeId = db.IncidentalContracTypes.Where(w => w.IncidentalContracDescription.ToUpper() == "SERVICE").FirstOrDefault()?.IncidentalContracTypeId;
                    incidentalContract.IncidentalContracTypeId = serviceIncidentalContract.IncidentalContracTypeId;
                }

                db.Entry(serviceIncidentalContract).State = EntityState.Modified;

                db.Entry(incidentalContract).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("EditService", new { Id = incidentalContract.IncidentalContractId });
            }
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", serviceContractViewModel.IncidentalContracOwnerId);
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", serviceContractViewModel.IncidentalContracControllerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserName", serviceContractViewModel.IncidentalContracProviderId);
            ViewBag.IncidentalContracTypeId = new SelectList(db.IncidentalContracTypes, "IncidentalContracTypeId", "IncidentalContracTypeTitle", serviceContractViewModel.IncidentalContracTypeId);
            ViewBag.OwnerMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", serviceContractViewModel.OwnerMunicipalEntityId);
            ViewBag.ProviderMunicipalEntityId = new SelectList(db.MunicipalEntities, "MunicipalEntityId", "MunicipalEntityName", serviceContractViewModel.ProviderMunicipalEntityId);
            return View(serviceContractViewModel);
        }

        #endregion


        #region Product Contracts

        // GET: Contracts/CreateProduct
        public ActionResult CreateProduct()
        {
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle");
            return View();
        }

        // POST: Contracts/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct([Bind(Include = "TrainningIncidentalContractId,IncidentalContractId,IncidentalContractTitle,IncidentalContracType,IncidentalContracStatus,IncidentalContracOwnerId,IncidentalContracControllerId,IncidentalContracProviderId,IncidentalContracDuration,IncidentalContracTimeframeStart,IncidentalContracTimeframeEnd,OwnerTermsAgreementDate,ProviderTermsAgreementDate,ControllerDeliverenceConfirmationDate,ProviderDeliverenceConfirmationDate")] ProductIncidentalContract productIncidentalContract)
        {
            if (ModelState.IsValid)
            {
                db.ProductIncidentalContracts.Add(productIncidentalContract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", productIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", productIncidentalContract.IncidentalContractId);
            return View(productIncidentalContract);
        }

        #endregion


        #region Trainning Contracts

        // GET: Contracts/CreateTrainning
        public ActionResult CreateTrainning()
        {
            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId");
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle");
            return View();
        }

        // POST: Contracts/CreateTrainning
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTrainning([Bind(Include = "TrainningIncidentalContractId,ServiceIncidentalContractPurpose,ServiceIncidentalContractHours,ServiceIncidentalContractProfile,IncidentalContractId,IncidentalContractTitle,IncidentalContracType,IncidentalContracStatus,IncidentalContracOwnerId,IncidentalContracControllerId,IncidentalContracProviderId,IncidentalContracDuration,IncidentalContracTimeframeStart,IncidentalContracTimeframeEnd,OwnerTermsAgreementDate,ProviderTermsAgreementDate,ControllerDeliverenceConfirmationDate,ProviderDeliverenceConfirmationDate")] TrainningIncidentalContract trainningIncidentalContract)
        {
            if (ModelState.IsValid)
            {
                db.TrainningIncidentalContracts.Add(trainningIncidentalContract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IncidentalContracControllerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracControllerId);
            ViewBag.IncidentalContracOwnerId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracOwnerId);
            ViewBag.IncidentalContracProviderId = new SelectList(db.UserAccounts, "UserAccountId", "UserId", trainningIncidentalContract.IncidentalContracProviderId);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts, "IncidentalContractId", "ContractTitle", trainningIncidentalContract.IncidentalContractId);
            return View(trainningIncidentalContract);
        }

        #endregion



        #region Contract Items

        // GET: IncidentalContractItems
        public ActionResult ListItems()
        {
            var incidentalContractItems = db.IncidentalContractItems.Include(i => i.IncidentalContract).Include(i => i.OwnerBudget).Include(i => i.ProductCodeExpense).Include(i => i.ProductCodeIncome);
            return View(incidentalContractItems.ToList());
        }



        // GET: Contract/CreateItem/5
        public ActionResult CreateItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContract incidentalContract = db.IncidentalContracts.Find(id);
            UserAccount BudgetOwner = db.UserAccounts.Find(incidentalContract.IncidentalContracOwnerId);
            if (incidentalContract == null)
            {
                return HttpNotFound();
            }
            IncidentalContractItem incidentalContractItem = new IncidentalContractItem();
            incidentalContractItem.IncidentalContract = incidentalContract;
            incidentalContractItem.IncidentalContractId = id;

            ViewBag.IncidentalContractItemVat = new SelectList(new SelectListItem[] { new SelectListItem() { Text = "0 %", Value = "0" }, new SelectListItem() { Value = "21", Text = "21 %" } }, "Value", "Text");
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts.Where(w => w.IncidentalContractId == id).ToList(), "IncidentalContractId", "ContractTitle");
            ViewBag.OwnerBudgetId = new SelectList(db.Budgets.Where(w => w.BudgetOwnerId == incidentalContract.IncidentalContracOwnerId), "BudgetId", "BudgetName");
            ViewBag.ProductExpenseCodeId = new SelectList(db.ProductCodes.Where(w => w.ProductCodeType == "U").ToList(), "ProductCodeId", "ProductCodeText");
            ViewBag.ProductIncomeCodeId = new SelectList(db.ProductCodes.Where(w => w.ProductCodeType == "I").ToList(), "ProductCodeId", "ProductCodeText");
            return View(incidentalContractItem);
        }

        // POST: Contract/CreateItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItem(IncidentalContractItem incidentalContractItem)
        {
            var incidentalContract = db.IncidentalContracts.Find(incidentalContractItem.IncidentalContractId);
            if (ModelState.IsValid)
            {
                incidentalContractItem.ProductIncomeCode = db.ProductCodes.Find(incidentalContractItem.ProductIncomeCodeId).ProductCodeNumber;
                incidentalContractItem.ProductExpenseCode = db.ProductCodes.Find(incidentalContractItem.ProductExpenseCodeId).ProductCodeNumber;
                incidentalContractItem.BudgetCodeId = db.Budgets.Find(incidentalContractItem.OwnerBudgetId).BudgetIdCode;

                db.IncidentalContractItems.Add(incidentalContractItem);
                db.SaveChanges();


                switch (incidentalContract.IncidentalContracTypeId)
                {
                    case 1:
                        return RedirectToAction("EditService", new { id = incidentalContractItem.IncidentalContractId });
                    case 2:
                        return RedirectToAction("EditProduct", new { id = incidentalContractItem.IncidentalContractId });
                    case 3:
                        return RedirectToAction("EditTrainning", new { id = incidentalContractItem.IncidentalContractId });
                    default:
                        return RedirectToAction("EditService", new { id = incidentalContractItem.IncidentalContractId });
                }
            }

            ViewBag.IncidentalContractItemVat = new SelectList(new SelectListItem[] { new SelectListItem() { Text = "0 %", Value = "0" }, new SelectListItem() { Value = "21", Text = "21 %" } }, "Value", "Text", incidentalContractItem.IncidentalContractItemVat);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts.Where(w => w.IncidentalContractId == incidentalContractItem.IncidentalContractId).ToList(), "IncidentalContractId", "ContractTitle", incidentalContractItem.IncidentalContractId);
            ViewBag.OwnerBudgetId = new SelectList(db.Budgets.Where(w => w.BudgetOwnerId == incidentalContract.IncidentalContracOwnerId), "BudgetId", "BudgetName", incidentalContractItem.OwnerBudgetId);
            ViewBag.ProductExpenseCodeId = new SelectList(db.ProductCodes.Where(w => w.ProductCodeType == "U").ToList(), "ProductCodeId", "ProductCodeText", incidentalContractItem.ProductExpenseCodeId);
            ViewBag.ProductIncomeCodeId = new SelectList(db.ProductCodes.Where(w => w.ProductCodeType == "I").ToList(), "ProductCodeId", "ProductCodeText", incidentalContractItem.ProductIncomeCodeId);
            return View(incidentalContractItem);
        }

        // GET: Contract/EditItem/5
        public ActionResult EditItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContractItem incidentalContractItem = db.IncidentalContractItems.Find(id);
            var incidentalContract = db.IncidentalContracts.Find(incidentalContractItem.IncidentalContractId);

            if (incidentalContractItem == null)
            {
                return HttpNotFound();
            }

            ViewBag.IncidentalContractItemVat = new SelectList(new SelectListItem[] { new SelectListItem() { Text = "0 %", Value = "0" }, new SelectListItem() { Value = "21", Text = "21 %" } }, "Value", "Text", incidentalContractItem.IncidentalContractItemVat);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts.Where(w => w.IncidentalContractId == id).ToList(), "IncidentalContractId", "ContractTitle", incidentalContractItem.IncidentalContractId);
            ViewBag.OwnerBudgetId = new SelectList(db.Budgets.Where(w => w.BudgetOwnerId == incidentalContract.IncidentalContracOwnerId), "BudgetId", "BudgetName", incidentalContractItem.OwnerBudgetId);
            ViewBag.ProductExpenseCodeId = new SelectList(db.ProductCodes.Where(w => w.ProductCodeType == "U").ToList(), "ProductCodeId", "ProductCodeText", incidentalContractItem.ProductExpenseCodeId);
            ViewBag.ProductIncomeCodeId = new SelectList(db.ProductCodes.Where(w => w.ProductCodeType == "I").ToList(), "ProductCodeId", "ProductCodeText", incidentalContractItem.ProductIncomeCodeId);
            return View(incidentalContractItem);
        }

        // POST: Contract/EditItem/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(IncidentalContractItem incidentalContractItem)
        {
            var incidentalContract = db.IncidentalContracts.Find(incidentalContractItem.IncidentalContractId);
            if (ModelState.IsValid)
            {
                db.Entry(incidentalContractItem).State = EntityState.Modified;
                incidentalContractItem.ProductIncomeCode = db.ProductCodes.Find(incidentalContractItem.ProductIncomeCodeId).ProductCodeNumber;
                incidentalContractItem.ProductExpenseCode = db.ProductCodes.Find(incidentalContractItem.ProductExpenseCodeId).ProductCodeNumber;
                incidentalContractItem.BudgetCodeId = db.Budgets.Find(incidentalContractItem.OwnerBudgetId).BudgetIdCode;

                db.SaveChanges();

                switch (incidentalContract.IncidentalContracTypeId)
                {
                    case 1:
                        return RedirectToAction("EditService", new { id = incidentalContractItem.IncidentalContractId });
                    case 2:
                        return RedirectToAction("EditProduct", new { id = incidentalContractItem.IncidentalContractId });
                    case 3:
                        return RedirectToAction("EditTrainning", new { id = incidentalContractItem.IncidentalContractId });
                    default:
                        return RedirectToAction("EditService", new { id = incidentalContractItem.IncidentalContractId });
                }
            }
            ViewBag.IncidentalContractItemVat = new SelectList(new SelectListItem[] { new SelectListItem() { Text = "0 %", Value = "0" }, new SelectListItem() { Value = "21", Text = "21 %" } }, "Value", "Text", incidentalContractItem.IncidentalContractItemVat);
            ViewBag.IncidentalContractId = new SelectList(db.IncidentalContracts.Where(w => w.IncidentalContractId == incidentalContractItem.IncidentalContractId).ToList(), "IncidentalContractId", "ContractTitle", incidentalContractItem.IncidentalContractId);
            ViewBag.OwnerBudgetId = new SelectList(db.Budgets.Where(w => w.BudgetOwnerId == incidentalContract.IncidentalContracOwnerId), "BudgetId", "BudgetName", incidentalContractItem.OwnerBudgetId);
            ViewBag.ProductExpenseCodeId = new SelectList(db.ProductCodes.Where(w => w.ProductCodeType == "U").ToList(), "ProductCodeId", "ProductCodeText", incidentalContractItem.ProductExpenseCodeId);
            ViewBag.ProductIncomeCodeId = new SelectList(db.ProductCodes.Where(w => w.ProductCodeType == "I").ToList(), "ProductCodeId", "ProductCodeText", incidentalContractItem.ProductIncomeCodeId);
            return View(incidentalContractItem);
        }

        // GET: Contract/DeleteItem/5
        public ActionResult DeleteItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidentalContractItem incidentalContractItem = db.IncidentalContractItems.Find(id);
            var incidentalContractId = incidentalContractItem.IncidentalContractId;
            if (incidentalContractItem == null)
            {
                return HttpNotFound();
            }
            db.IncidentalContractItems.Remove(incidentalContractItem);
            db.SaveChanges();

            var incidentalContract = db.IncidentalContracts.Find(incidentalContractId);

            switch (incidentalContract.IncidentalContracTypeId)
            {
                case 1:
                    return RedirectToAction("EditService", new { id = incidentalContractItem.IncidentalContractId });
                case 2:
                    return RedirectToAction("EditProduct", new { id = incidentalContractItem.IncidentalContractId });
                case 3:
                    return RedirectToAction("EditTrainning", new { id = incidentalContractItem.IncidentalContractId });
                default:
                    return RedirectToAction("EditService", new { id = incidentalContractItem.IncidentalContractId });
            }
        }

        #endregion



        private void refrence()
        {
            /*
            TinyMapper.Bind<Person, PersonDto>();

            var person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe"
            };

            var personDto = TinyMapper.Map<PersonDto>(person);

            // with mapping members ignored and bind members with different names/types

            TinyMapper.Bind<Person, PersonDto>(config =>
            {
                config.Ignore(x => x.Id);
                config.Ignore(x => x.Email);
                config.Bind(source => source.LastName, target => target.Surname);
                config.Bind(target => source.Emails, typeof(List<string>));
            });

            var person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Emails = new List<string> { "support@tinymapper.net", "MyEmail@tinymapper.net" }
            };

            var personDto = TinyMapper.Map<PersonDto>(person);
            */
        }
    }
}