using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.Entities
{
    public class ProductCodeCategory
    {
        [Key]
        public int ProductCodeCategoryId { get; set; }

        [Display(Name = "Category Number")]
        public int ProductCodeCategoryNumber { get; set; }

        [Display(Name = "Category Description")]
        public string ProductCodeCategoryDescription { get; set; }
    }
}