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
    public class TeamLeaderController : Controller
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

        // GET: TeamLeader
        public ActionResult Index()
        {
            TeamLeadDashboardViewModel model = new TeamLeadDashboardViewModel();

            model.ContractsInNegotiationList = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.Initiated
                                                                              && w.IncidentalContracProviderId == UserAccountId).ToList()
                                            ?? new List<IncidentalContract>();
            model.ContractsInApprovalList = db.IncidentalContracts.Where(w => w.IncidentalContracStatus == ContractStatus.ApprovalPending
                                                                           && w.IncidentalContracProviderId == UserAccountId).ToList()
                                            ?? new List<IncidentalContract>();
            model.ProjectsList = db.ExecutionProjects.Where(w => w.ExecutionProjectProviderId == UserAccountId && w.ExecutionProjectStatus != ProjectStatus.Confirmed).ToList();

            return View(model);
        }
        public ActionResult Contracts()
        {
            return RedirectToAction("List", "Contract");
        }
        public ActionResult Projects()
        {
            return RedirectToAction("List", "Project");
        }
    }
}