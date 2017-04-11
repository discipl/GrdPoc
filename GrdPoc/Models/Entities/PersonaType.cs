using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class PersonaType
    {
        [Key]
        public int PersonaTypeId { get; set; }

        [Display(Name = "Persona Name")]
        public string PersonaTypeName { get; set; }

        public ICollection<UserAccount> UserAccounts { get; set; } = new HashSet<UserAccount>();
    }
}