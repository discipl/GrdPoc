using GrdPoc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.ViewModels
{
    public class ProjectResourceDashboardViewModel
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        //private int UserAccountId
        //{
        //    get
        //    {
        //        return (int?)Session["currentUserId"] ?? 0;
        //    }
        //}
        //private string UserName
        //{
        //    get
        //    {
        //        return (CurrentUser?.UserName ?? "");
        //    }
        //}

        //private UserAccount currentUser;
        //private UserAccount CurrentUser
        //{
        //    get
        //    {
        //        int userId = (int?)Session["currentUserId"] ?? 0;
        //        if (currentUser == null)
        //        {
        //            currentUser = db.UserAccounts.Find(userId);
        //        }

        //        return (currentUser ?? new UserAccount());
        //    }
        //}

        //private string Persona
        //{
        //    get
        //    {
        //        return Session["Persona"]?.ToString();
        //    }
        //}

        public List<ExecutionProject> ProjectsList { get; set; }


    }
}