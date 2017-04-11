using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class TrainningIncidentalContract : BaseIncidentalContract
    {
        [Key]
        public int TrainningIncidentalContractId { get; set; }

        [Display(Name = "Contract Purpose/Description")]
        public string ServiceIncidentalContractPurpose { get; set; }

        [Display(Name = "Contracted Hours")]
        public int ServiceIncidentalContractHours { get; set; }

        [Display(Name = "Profile(s) Description")]
        public string ServiceIncidentalContractProfile { get; set; }



        [ForeignKey("IncidentalContract")]
        public int? IncidentalContractId { get; set; }
        public virtual IncidentalContract IncidentalContract { get; set; }


    }
}