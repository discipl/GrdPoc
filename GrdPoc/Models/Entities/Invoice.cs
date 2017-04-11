using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Display(Name = "Description")]
        public string InvoiceDescription { get; set; }


        [NotMapped]
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber
        {
            get
            {
                return InvoiceId.ToString("D8");
            }
        }
        [Display(Name = "Issuer")]
        public string InvoiceIssuerName { get; set; }

        [Display(Name = "Issuer Address")]
        public string InvoiceIssuerAddress { get; set; }

        [Display(Name = "Issuer KVK")]
        public string InvoiceIssuerKvk { get; set; }

        [Display(Name = "Issuer BTW")]
        public string InvoiceIssuerBtw { get; set; }

        [Display(Name = "Issuer IBAN")]
        public string InvoiceIssuerIban { get; set; }


        [Display(Name = "Invoice Date")]
        public DateTime? InvoiceDate { get; set; }

        [Display(Name = "Due Date")]
        public DateTime? InvoiceDueDate { get; set; }


        [Display(Name = "Customer")]
        public string InvoiceCustomerName { get; set; }

        [Display(Name = "Issuer Address")]
        public string InvoiceCustomeAddress { get; set; }

        [Display(Name = "Issuer KVK")]
        public string InvoiceCustomeKvk { get; set; }

        [Display(Name = "Issuer BTW")]
        public string InvoiceCustomeBtw { get; set; }


        [Display(Name = "Value")]
        public decimal InvoiceValue { get; set; }

        [Display(Name = "Total VAT")]
        public decimal InvoiceVatTotal { get; set; }

        [Display(Name = "Value with VAT")]
        public decimal InvoiceValueTotal { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new HashSet<InvoiceItem>();

    }
}