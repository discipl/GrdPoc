using GrdPoc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.ViewModels
{
    public class ProjectLeaderDashboardViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<IncidentalContract> ContractsInNegotiationList { get; set; }

        public List<IncidentalContract> ContractsInApprovalList { get; set; }

        public List<ExecutionProject> ProjectsList { get; set; }


    }
}