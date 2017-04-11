using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class BudgetBalanceTransaction
    {
        [Key]
        public int BudgetBalanceTransactionId { get; set; }

        [Display(Name = "Budget")]
        [ForeignKey("OwnerBudget")]
        public int? OwnerBudgetId { get; set; }
        public virtual Budget OwnerBudget { get; set; }

        [Display(Name = "Deposit")]
        public decimal BudgetBalanceDeposit { get; set; }

        [Display(Name = "Deposit Source")]
        public string BudgetBalanceDepositSource { get; set; }

        [Display(Name = "Withdrawal")]
        public decimal BudgetBalanceWithdrawal { get; set; }

        [Display(Name = "Withdrawal Source (eg. InvoiceId)")]
        public string BudgetBalanceWithdrawalSource { get; set; }
    }
}