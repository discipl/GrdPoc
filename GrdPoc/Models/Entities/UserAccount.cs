using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class UserAccount
    {
        [Key]
        public int UserAccountId { get; set; }

        //ForeignKey to AspIdentityUser, not enforced because it might be a seperate database in the future
        public string UserId { get; set; }


        [Display(Name = "Name")]
        public string UserName { get; set; }

        [Display(Name = "Title")]
        public string UserTitle { get; set; }

        [Display(Name = "Persona Type")]
        [ForeignKey("PersonaType")]
        public int? PersonaTypeId { get; set; }
        public virtual PersonaType PersonaType { get; set; }


        [Display(Name = "Municipal Entity")]
        [ForeignKey("MunicipalEntity")]
        public int? MunicipalEntityId { get; set; }
        public virtual MunicipalEntity MunicipalEntity { get; set; }

    }
}