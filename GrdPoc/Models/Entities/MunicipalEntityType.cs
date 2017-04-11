using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class MunicipalEntityType
    {
        [Key]
        public int MunicipalEntityTypeId { get; set; }

        [Display(Name = "Name")]
        public string MunicipalEntityName { get; set; }

    }
}