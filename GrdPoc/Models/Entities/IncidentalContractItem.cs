using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class IncidentalContractItem
    {
        [Key]
        public int IncidentalContractItemId { get; set; }

        [Display(Name = "Descritpion")]
        public string IncidentalContractItemDescription { get; set; }

        [Display(Name = "Value")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal IncidentalContractItemValue { get; set; }

        [Display(Name = "Quantity")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int IncidentalContractItemQuantity { get; set; }

        [NotMapped]
        [Display(Name = "Cost")]
        public decimal CostValue
        {
            get { return IncidentalContractItemValue * IncidentalContractItemQuantity; }
        }

        [Display(Name = "VAT")]
        public int IncidentalContractItemVat { get; set; }

        [NotMapped] 
        [Display(Name = "Cost with VAT")]
        public decimal CostValueWithVat
        {
            get { return IncidentalContractItemValue * IncidentalContractItemQuantity * (IncidentalContractItemVat == 0 ? 1 : (decimal)IncidentalContractItemVat / 100) ; }
        }


        [Display(Name = "Income Code")]
        public string ProductIncomeCode { get; set; }

        [Display(Name = "Income Code")]
        [ForeignKey("ProductCodeIncome")]
        public int? ProductIncomeCodeId { get; set; }
        public virtual ProductCode ProductCodeIncome { get; set; }


        [Display(Name = "Expense Code")]
        public string ProductExpenseCode { get; set; }

        [Display(Name = "Expense Code")]
        [ForeignKey("ProductCodeExpense")]
        public int? ProductExpenseCodeId { get; set; }
        public virtual ProductCode ProductCodeExpense { get; set; }


        [Display(Name = "Budget Code")]
        public string BudgetCodeId { get; set; }

        [Display(Name = "Budget")]
        [ForeignKey("OwnerBudget")]
        public int? OwnerBudgetId { get; set; }
        public virtual Budget OwnerBudget { get; set; }


        [ForeignKey("IncidentalContract")]
        public int? IncidentalContractId { get; set; }
        public virtual IncidentalContract IncidentalContract { get; set; }

    }
}