using GrdPoc.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.ViewModels
{
    public class ProductContractViewModel
    {

        [Key]
        public int? IncidentalContractId { get; set; }

        [Display(Name = "Title")]
        public string ContractTitle { get; set; }

        [Display(Name = "Type")]
        [ForeignKey("IncidentalContracType")]
        public int? IncidentalContracTypeId { get; set; }
        public virtual IncidentalContracType IncidentalContracType { get; set; }

        [Display(Name = "Owner Municipal Entity")]
        [ForeignKey("OwnerMunicipalEntity")]
        public int? OwnerMunicipalEntityId { get; set; }
        public virtual MunicipalEntity OwnerMunicipalEntity { get; set; }

        [Display(Name = "Owner")]
        [ForeignKey("OwnerUserAccount")]
        public int? IncidentalContracOwnerId { get; set; }
        public virtual UserAccount OwnerUserAccount { get; set; }

        [Display(Name = "Owner Name")]
        public string IncidentalContracOwnerName { get; set; }

        [Display(Name = "Controller")]
        [ForeignKey("ControllerUserAccount")]
        public int? IncidentalContracControllerId { get; set; }
        public virtual UserAccount ControllerUserAccount { get; set; }

        [Display(Name = "Controller Name")]
        public string IncidentalContracControllerName { get; set; }


        [Display(Name = "Provider Municipal Entity")]
        [ForeignKey("ProviderMunicipalEntity")]
        public int? ProviderMunicipalEntityId { get; set; }
        public virtual MunicipalEntity ProviderMunicipalEntity { get; set; }

        [Display(Name = "Provider")]
        [ForeignKey("IncidentalContracProvider")]
        public int? IncidentalContracProviderId { get; set; }
        public virtual UserAccount IncidentalContracProvider { get; set; }

        [Display(Name = "Provider Name")]
        public string IncidentalContracProviderName { get; set; }

        [Display(Name = "Invoice Number")]
        public string IncidentalContracInvoiceNumber { get; set; }


        [Display(Name = "Status")]
        public ContractStatus IncidentalContracStatus { get; set; }

        [Display(Name = "Duration (days)")]
        public int? IncidentalContracDuration { get; set; }

        [Display(Name = "Timeframe Start")]
        public DateTime? IncidentalContracTimeframeStart { get; set; }

        [Display(Name = "Timeframe End")]
        public DateTime? IncidentalContracTimeframeEnd { get; set; }


        [Display(Name = "Owner Agreed Terms on")]
        public DateTime? OwnerTermsAgreementDate { get; set; }

        [Display(Name = "Provider Agreed Terms on")]
        public DateTime? ProviderTermsAgreementDate { get; set; }


        [Display(Name = "Controller Confirms Deliverence on")]
        public DateTime? ControllerDeliverenceConfirmationDate { get; set; }

        [Display(Name = "Provider Confirms Deliverence on")]
        public DateTime? ProviderDeliverenceConfirmationDate { get; set; }


        private List<IncidentalContractItem> contractItems;

        public List<IncidentalContractItem> ContractItems
        {
            get
            {
                return (contractItems == null ? new List<IncidentalContractItem>() : contractItems);
            }
            set
            {
                contractItems = value;
            }
        }



        public int? ServiceIncidentalContractId { get; set; }

        [Display(Name = "Contract Purpose/Description")]
        public string ServiceIncidentalContractPurpose { get; set; }

        [Display(Name = "Contracted Hours")]
        public int ServiceIncidentalContractHours { get; set; }

        [Display(Name = "Profile(s) Description")]
        public string ServiceIncidentalContractProfile { get; set; }

    }
}