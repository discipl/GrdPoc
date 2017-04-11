using GrdPoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult CollapseSideMenu()
        {
            if (HttpContext.Request.IsAjaxRequest())
            {
                if (Session["Collapsed"]?.ToString() == "True")
                {
                    Session["Collapsed"] = "False";
                }
                else
                {
                    Session["Collapsed"] = "True";
                }
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            return View();
        }


        public ActionResult ChangePersona(int? Id)
        {
            switch (Id)
            {
                case 1:
                    ViewBag.Persona = "BudgetOwner";
                    Session["Persona"] = "Budget Owner";
                    Session["currentUserId"] = Session["budgetOwnerUserId"];
                    Session["currentUserName"] = Session["budgetOwnerName"];
                    break;
                case 2:
                    ViewBag.Persona = "TeamLeader";
                    Session["Persona"] = "Team Leader";
                    Session["currentUserId"] = Session["teamLeadUserId"];
                    Session["currentUserName"] = Session["teamLeadName"];
                    break;
                case 3:
                    ViewBag.Persona = "ProjectLeader";
                    Session["Persona"] = "Project Leader";
                    Session["currentUserId"] = Session["projectLeaderUserId"];
                    Session["currentUserName"] = Session["projectLeaderName"];
                    break;
                case 4:
                    ViewBag.Persona = "ProjectResource";
                    Session["Persona"] = "Project Resource";
                    Session["currentUserId"] = Session["projectResourceUserId"];
                    Session["currentUserName"] = Session["projectResourceName"];
                    break;
                case 5:
                    ViewBag.Persona = "Treasury";
                    Session["Persona"] = "Treasury";
                    Session["currentUserId"] = Session["treasuryUserId"];
                    Session["currentUserName"] = Session["treasuryName"];
                    break;
                case 6:
                    ViewBag.Persona = "System";
                    Session["Persona"] = "System";
                    Session["currentUserId"] = "";
                    Session["currentUserName"] = "";
                    break;
                default:
                    ViewBag.Persona = "BudgetOwner";
                    Session["Persona"] = "Budget Owner";
                    Session["currentUserId"] = Session["budgetOwnerUserId"];
                    Session["currentUserName"] = Session["budgetOwnerName"];
                    break;
            }

            return RedirectToAction("Index", ViewBag.Persona);
        }

        // POST:  BudgetOwner/SelectBudgetOwner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectBudgetOwner([Bind(Include = "UserAccountId")]int? userAccountId)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserAccounts.Find(userAccountId);

                if (user == null)
                {
                    return RedirectToAction("Index", "BudgetOwner");
                }

                Session["Persona"] = "Budget Owner";
                Session["budgetOwnerUserId"] = userAccountId;
                Session["budgetOwnerName"] = user.UserName;
                Session["currentUserId"] = userAccountId;
                Session["currentUserName"] = user.UserName;

                return RedirectToAction("Index", "BudgetOwner");
            }

            return RedirectToAction("Index", "BudgetOwner");
        }

        // POST:  BudgetOwner/SelectBudgetOwner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectTeamLead([Bind(Include = "UserAccountId")]int? userAccountId)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserAccounts.Find(userAccountId);

                if (user == null)
                {
                    return RedirectToAction("Index", "TeamLeader");
                }

                Session["Persona"] = "Team Leader";
                Session["teamLeadUserId"] = userAccountId;
                Session["teamLeadName"] = user.UserName;
                Session["currentUserId"] = userAccountId;
                Session["currentUserName"] = user.UserName;

                return RedirectToAction("Index", "TeamLeader");
            }

            return RedirectToAction("Index", "TeamLeader");
        }


        // POST:  BudgetOwner/SelectBudgetOwner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectProjectLeader([Bind(Include = "UserAccountId")]int? userAccountId)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserAccounts.Find(userAccountId);

                if (user == null)
                {
                    return RedirectToAction("Index", "ProjectLeader");
                }

                Session["Persona"] = "Project Leader";
                Session["projectLeaderUserId"] = userAccountId;
                Session["projectLeaderName"] = user.UserName;
                Session["currentUserId"] = userAccountId;
                Session["currentUserName"] = user.UserName;

                return RedirectToAction("Index", "ProjectLeader");
            }

            return RedirectToAction("Index", "ProjectLeader");
        }

        // POST:  BudgetOwner/SelectBudgetOwner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectProjectResource([Bind(Include = "UserAccountId")]int? userAccountId)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserAccounts.Find(userAccountId);

                if (user == null)
                {
                    return RedirectToAction("Index", "ProjectResource");
                }

                Session["Persona"] = "Project Resource";
                Session["projectResourceUserId"] = userAccountId;
                Session["projectResourceName"] = user.UserName;
                Session["currentUserId"] = userAccountId;
                Session["currentUserName"] = user.UserName;

                return RedirectToAction("Index", "ProjectResource");
            }

            return RedirectToAction("Index", "ProjectResource");
        }

        // POST:  BudgetOwner/SelectBudgetOwner
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectTreasury([Bind(Include = "UserAccountId")]int? userAccountId)
        {
            if (ModelState.IsValid)
            {
                var user = db.UserAccounts.Find(userAccountId);

                if (user == null)
                {
                    return RedirectToAction("Index", "Treasury");
                }

                Session["Persona"] = "Treasury";
                Session["treasuryUserId"] = userAccountId;
                Session["treasuryName"] = user.UserName;
                Session["currentUserId"] = userAccountId;
                Session["currentUserName"] = user.UserName;

                return RedirectToAction("Index", "Treasury");
            }

            return RedirectToAction("Index", "Treasury");
        }


    }
}