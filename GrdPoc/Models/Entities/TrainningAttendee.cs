using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class TrainningAttendee
    {
        [Key]
        public int TrainningAttendeeId { get; set; }

        [Display(Name = "Person's Name")]
        public string TrainningAttendeeName { get; set; }

        [Display(Name = "Person's Title")]
        public string TrainningAttendeeTitle{ get; set; }

        [Display(Name = "User Account")]
        [ForeignKey("UserAccount")]
        public int? UserAccountId { get; set; }
        public virtual UserAccount UserAccount { get; set; }

        [Display(Name = "Trainnig Contract")]
        [ForeignKey("TrainningIncidentalContract")]
        public int? TrainningIncidentalContractId { get; set; }
        public virtual TrainningIncidentalContract TrainningIncidentalContract { get; set; }

    }
}