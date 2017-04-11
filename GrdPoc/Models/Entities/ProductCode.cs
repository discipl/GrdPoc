using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class ProductCode
    {
        [Key]
        public int ProductCodeId { get; set; }

        [Display(Name = "Code")]
        public string ProductCodeNumber { get; set; }

        [NotMapped]
        public string ProductCodeText
        {
            get { return string.Format("{0} - {1}", ProductCodeNumber, ProductCodeDescription); }
        }

        [Display(Name = "Type [(I)nkomsten/(U)itgaven]")]
        public string ProductCodeType { get; set; }

        [Display(Name = "Description")]
        public string ProductCodeDescription { get; set; }

        [Display(Name = "Category")]
        [ForeignKey("ProductCodeCategory")]
        public int? ProductCodeCategoryId { get; set; }
        public virtual ProductCodeCategory ProductCodeCategory { get; set; }

    }
}