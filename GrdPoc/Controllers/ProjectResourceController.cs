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
    public class ProjectResourceController : Controller
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


        // GET: ProjectResource
        public ActionResult Index()
        {
            ProjectResourceDashboardViewModel model = new ProjectResourceDashboardViewModel();

            model.ProjectsList = db.ExecutionProjects.Where(w => w.ExecutionProjectResourceId == UserAccountId && w.ExecutionProjectStatus != ProjectStatus.Confirmed).ToList();

            return View(model);
        }

        public ActionResult Projects()
        {
            return RedirectToAction("List", "Project");
        }

    }
}