using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class Budget
    {
        [Key]
        public int BudgetId { get; set; }

        [Display(Name = "Budget Name")]
        public string BudgetName { get; set; }

        [Display(Name = "Budget ID Code")]
        public string BudgetIdCode { get; set; }

        [Display(Name = "Initial Budget")]
        public decimal BudgetInitialBalance { get; set; }

        [Display(Name = "Budget Balance")]
        public decimal BudgetBalance { get; set; }


        [Display(Name = "Budget Owner")]
        [ForeignKey("BudgetOwner")]
        public int? BudgetOwnerId { get; set; }
        public virtual UserAccount BudgetOwner { get; set; }

        public ICollection<BudgetBalanceTransaction> BudgetBalanceTransactions { get; set; } = new HashSet<BudgetBalanceTransaction>();
        public ICollection<IncidentalContractItem> IncidentalContractItems { get; set; } = new HashSet<IncidentalContractItem>();

    }
}