using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }

        [ForeignKey("Invoice")]
        public int? InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }

        [Display(Name = "Descritpion")]
        public string Description { get; set; }

        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "VAT")]
        public int VatPercent { get; set; }

        [Display(Name = "Total")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }


        [Display(Name = "Income Code")]
        public string ProductIncomeCode { get; set; }

        [Display(Name = "Expense Code")]
        public string ProductExpenseCode { get; set; }

        [Display(Name = "Budget Code")]
        public string BudgetCode { get; set; }

    }
}