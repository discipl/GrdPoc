using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class IncidentalContracType
    {
        [Key]
        public int IncidentalContracTypeId { get; set; }

        [Display(Name = "Title")]
        public string IncidentalContracTypeTitle { get; set; }

        [Display(Name = "Type")]
        public string IncidentalContracDescription { get; set; }

        [Display(Name = "MvcController")]
        public string IncidentalContracMvcController{ get; set; }




        [Display(Name = "Blockchain Smart Contract")]
        [ForeignKey("SmartContract")]
        public int? SmartContractId { get; set; }
        public virtual SmartContract SmartContract { get; set; }

        [Display(Name = "Active")]
        public bool IncidentalContracTypeActive { get; set; }

        public IncidentalContracType()
        {
            IncidentalContracTypeActive = true;
        }
    }
}