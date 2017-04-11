using GrdPoc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.ViewModels
{
    public class NavigationViewModel
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        private static List<UserAccount> budgetOwnersList;
        public static List<UserAccount> BudgetOwnersList
        {
            get
            {
                if (budgetOwnersList == null)
                    budgetOwnersList = db.UserAccounts.Where(w => w.PersonaTypeId == 1).OrderBy(o => o.UserName).ToList();
                return budgetOwnersList;
            }
        }

        private static List<UserAccount> teamLeadList;
        public static List<UserAccount> TeamLeadList
        {
            get
            {
                if (teamLeadList == null)
                    teamLeadList = db.UserAccounts.Where(w => w.PersonaTypeId == 2).OrderBy(o => o.UserName).ToList();
                return teamLeadList;
            }
        }

        private static List<UserAccount> projectLeaderList;
        public static List<UserAccount> ProjectLeaderList
        {
            get
            {
                if (projectLeaderList == null)
                    projectLeaderList = db.UserAccounts.Where(w => w.PersonaTypeId == 3).OrderBy(o => o.UserName).ToList();
                return projectLeaderList;
            }
        }

        private static List<UserAccount> projectResourceList;
        public static List<UserAccount> ProjectResourceList
        {
            get
            {
                if (projectResourceList == null)
                    projectResourceList = db.UserAccounts.Where(w => w.PersonaTypeId == 4).OrderBy(o => o.UserName).ToList();
                return projectResourceList;
            }
        }

        private static List<UserAccount> treasuryList;
        public static List<UserAccount> TreasuryList
        {
            get
            {
                if (treasuryList == null)
                    treasuryList = db.UserAccounts.Where(w => w.PersonaTypeId == 5).OrderBy(o => o.UserName).ToList();
                return treasuryList;
            }
        }


    }
}