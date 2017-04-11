using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class IncidentalContract
    {
        [Key]
        public int IncidentalContractId { get; set; }

        [Display(Name = "Title")]
        public string ContractTitle { get; set; }

        [Display(Name = "Type")]
        [ForeignKey("IncidentalContracType")]
        public int? IncidentalContracTypeId { get; set; }
        public virtual IncidentalContracType IncidentalContracType { get; set; }

        [Display(Name = "Status")]
        public ContractStatus IncidentalContracStatus { get; set; }

        [Display(Name = "Owner Municipal Entity")]
        [ForeignKey("OwnerMunicipalEntity")]
        public int? OwnerMunicipalEntityId { get; set; }
        public virtual MunicipalEntity OwnerMunicipalEntity { get; set; }

        [Display(Name = "Owner")]
        public int? IncidentalContracOwnerId { get; set; }
        [Display(Name = "Owner Name")]
        public string IncidentalContracOwnerName { get; set; }
        [Display(Name = "Controller")]
        public int? IncidentalContracControllerId { get; set; }
        [Display(Name = "Controller Name")]
        public string IncidentalContracControllerName { get; set; }


        [Display(Name = "Provider Municipal Entity")]
        [ForeignKey("ProviderMunicipalEntity")]
        public int? ProviderMunicipalEntityId { get; set; }
        public virtual MunicipalEntity ProviderMunicipalEntity { get; set; }

        [Display(Name = "Provider")]
        public int? IncidentalContracProviderId { get; set; }
        [Display(Name = "Provider Name")]
        public string IncidentalContracProviderName { get; set; }

        public ICollection<IncidentalContractItem> IncidentalContractItems { get; set; } = new HashSet<IncidentalContractItem>();

        [Display(Name = "Invoice Number")]
        public string IncidentalContracInvoiceNumber{ get; set; }


        public ICollection<ServiceIncidentalContract> ServiceIncidentalContracts { get; set; } = new HashSet<ServiceIncidentalContract>();
        public ICollection<ProductIncidentalContract> ProductIncidentalContracts { get; set; } = new HashSet<ProductIncidentalContract>();
        public ICollection<TrainningIncidentalContract> TrainningIncidentalContracts { get; set; } = new HashSet<TrainningIncidentalContract>();
    }
}