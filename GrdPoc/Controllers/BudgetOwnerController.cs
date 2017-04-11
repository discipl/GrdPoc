using GrdPoc.Models;
using GrdPoc.Models.Entities;
using GrdPoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class BudgetOwnerController : Controller
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


        // GET: BudgetOwner
        public ActionResult Index()
        {
            BudgetOwnerDashboardViewModel model = new BudgetOwnerDashboardViewModel();

            model.ContractsInNegotiationList = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Initiated
                                                                              && w.IncidentalContracOwnerId == UserAccountId).ToList() 
                                            ?? new List<IncidentalContract>();
            model.ContractsInApprovalList = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.ApprovalPending
                                                                           && (w.IncidentalContracOwnerId == UserAccountId  
                                                                            || w.IncidentalContracProviderId == UserAccountId )).ToList() 
                                            ?? new List<IncidentalContract>();

            var topBudgets = db.Budgets.Where(w => w.BudgetOwnerId == UserAccountId).OrderByDescending(o => o.BudgetBalanceTransactions.Count()).Take(3).ToList();
            if (topBudgets?.Count() == 3)
            {
                model.SetBudgetsList(topBudgets);
            }
            else
            {
                model.SetBudgetsList(new List<Budget>());
            }

            return View(model);
        }
        public ActionResult Contracts()
        {
            return RedirectToAction("List", "Contract");
        }
        public ActionResult Budgets()
        {
            return RedirectToAction("List", "Budget");
        }

    }
}