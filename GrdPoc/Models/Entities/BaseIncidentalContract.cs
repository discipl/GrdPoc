using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class BaseIncidentalContract
    {
        [Display(Name = "Title")]
        public string IncidentalContractTitle { get; set; }

        [Display(Name = "Type")]
        [ForeignKey("IncidentalContracType")]
        public int? IncidentalContracTypeId { get; set; }
        public virtual IncidentalContracType IncidentalContracType { get; set; }

        [Display(Name = "Status")]
        public ContractStatus IncidentalContracStatus { get; set; }

        [Display(Name = "Owner")]
        [ForeignKey("IncidentalContracOwner")]
        public int? IncidentalContracOwnerId { get; set; }
        public virtual UserAccount IncidentalContracOwner { get; set; }

        [Display(Name = "Controller")]
        [ForeignKey("IncidentalContracController")]
        public int? IncidentalContracControllerId { get; set; }
        public virtual UserAccount IncidentalContracController { get; set; }

        [Display(Name = "Provider")]
        [ForeignKey("IncidentalContracProvider")]
        public int? IncidentalContracProviderId { get; set; }
        public virtual UserAccount IncidentalContracProvider { get; set; }

        [Display(Name = "Duration (days)")]
        public int? IncidentalContracDuration { get; set; }

        [Display(Name = "Timeframe Start")]
        public DateTime? IncidentalContracTimeframeStart { get; set; }

        [Display(Name = "Timeframe End")]
        public DateTime? IncidentalContracTimeframeEnd { get; set; }


        [Display(Name = "Owner Agreed Terms")]
        public DateTime? OwnerTermsAgreementDate { get; set; }

        [Display(Name = "Provider Agreed Terms")]
        public DateTime? ProviderTermsAgreementDate { get; set; }


        [Display(Name = "Controller Confirms Deliverence")]
        public DateTime? ControllerDeliverenceConfirmationDate { get; set; }

        [Display(Name = "Provider Confirms Deliverence")]
        public DateTime? ProviderDeliverenceConfirmationDate { get; set; }



    }
}