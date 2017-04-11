using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class ProductIncidentalContract : BaseIncidentalContract
    {
        [Key]
        public int TrainningIncidentalContractId { get; set; }

        [ForeignKey("IncidentalContract")]
        public int? IncidentalContractId { get; set; }
        public virtual IncidentalContract IncidentalContract { get; set; }

    }
}