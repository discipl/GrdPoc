using GrdPoc.Models;
using GrdPoc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GrdPoc.Controllers
{
    [Authorize]
    public class BudgetController : Controller
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

        // GET: Budget
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            var budgets = db.Budgets.Where(w => w.BudgetOwnerId == UserAccountId).Include(b => b.BudgetOwner);
            return View(budgets.ToList());
        }

        // GET: Budgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }

            ViewBag.BudgetBalanceTransactions = db.BudgetBalanceTransactions.Where(w => w.OwnerBudgetId == budget.BudgetId);

            return View(budget);
        }


    }
}