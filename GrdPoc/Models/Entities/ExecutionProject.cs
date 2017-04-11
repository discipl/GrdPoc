using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class ExecutionProject
    {
        [Key]
        public int ExecutionProjectId { get; set; }

        [Display(Name = "Title")]
        public string ExecutionProjectTitle { get; set; }

        [ForeignKey("IncidentalContract")]
        public int? IncidentalContracId { get; set; }
        public virtual IncidentalContract IncidentalContract { get; set; }

        [Display(Name = "Status")]
        public ProjectStatus ExecutionProjectStatus { get; set; }


        [Display(Name = "Controller")]
        [ForeignKey("ExecutionProjectController")]
        public int? ExecutionProjectControllerId { get; set; }
        public virtual UserAccount ExecutionProjectController { get; set; }

        [Display(Name = "Provider")]
        [ForeignKey("ExecutionProjectProvider")]
        public int? ExecutionProjectProviderId { get; set; }
        public virtual UserAccount ExecutionProjectProvider { get; set; }

        [Display(Name = "Resource")]
        [ForeignKey("ExecutionProjectResource")]
        public int? ExecutionProjectResourceId { get; set; }
        public virtual UserAccount ExecutionProjectResource { get; set; }


        [Display(Name = "Duration (days)")]
        public int? ExecutionProjectDuration { get; set; }

        [Display(Name = "Timeframe Start")]
        public DateTime? ExecutionProjectTimeframeStart { get; set; }

        [Display(Name = "Timeframe End")]
        public DateTime? ExecutionProjectTimeframeEnd { get; set; }


        [Display(Name = "Scheduled Start")]
        public DateTime? ExecutionProjectSchedulledStart { get; set; }

        [Display(Name = "Scheduled End")]
        public DateTime? ExecutionProjectSchedulledEnd { get; set; }


        [Display(Name = "Actual Start")]
        public DateTime? ExecutionProjectActualStart { get; set; }

        [Display(Name = "Actual End")]
        public DateTime? ExecutionProjectActualEnd { get; set; }


        [Display(Name = "Deliverance Date")]
        public DateTime? ExecutionProjectDeliveranceDate { get; set; }

        [Display(Name = "Deliverance Confirmation")]
        public DateTime? ExecutionProjectDeliveranceConfirmation { get; set; }

    }
}