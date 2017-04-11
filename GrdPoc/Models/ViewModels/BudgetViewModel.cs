using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.ViewModels
{
    public class BudgetViewModel
    {
        public string BudgetName { get; set; }
        public string ValuesArray { get; set; }
        public string InitialBudget { get; set; }
        public string CurrentBudget { get; set; }
        public string OutstandingBudget { get; set; }
        public string BudgetSpent { get; set; }

        public int BudgetsAvailablePercentage { get; set; }

    }
}