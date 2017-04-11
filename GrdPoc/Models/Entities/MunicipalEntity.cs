using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class MunicipalEntity
    {
        [Key]
        public int MunicipalEntityId { get; set; }

        [Display(Name = "Municipal Entity Name")]
        public string MunicipalEntityName { get; set; }

        [Display(Name = "Municipal Entity Address")]
        public string MunicipalEntityAddress { get; set; }

        [Display(Name = "Municipal Entity KVK")]
        public string MunicipalEntityKvk { get; set; }

        [Display(Name = "Municipal Entity BTW")]
        public string MunicipalEntityBtw { get; set; }

        [Display(Name = "Municipal Entity IBAN")]
        public string MunicipalEntityIban { get; set; }

        [Display(Name = "Entity Type")]
        public int MunicipalEntityType { get; set; }

        public ICollection<UserAccount> UserAccounts { get; set; } = new HashSet<UserAccount>();
        public ICollection<IncidentalContract> IncidentalContractOwners { get; set; } = new HashSet<IncidentalContract>();
        public ICollection<IncidentalContract> IncidentalContractProviders { get; set; } = new HashSet<IncidentalContract>();

    }
}