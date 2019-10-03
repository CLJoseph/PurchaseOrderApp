using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels
{
    public class OrganisationItemViewModel
    {
        public string Id { get; set; }
        public string OrganisationId { get; set; }    
        public string RowVersionNo { get; set; }
        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "An Item Name is required")]
        public string Name { get; set; }
        [Display(Name = "Brand")]
        
        public string Brand { get; set; }
        [Display(Name = "Item Code")]
        [Required(ErrorMessage = "An Item code is required")]
        public string Code { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Current price")]
        [Range(1.00, 1000.00 ,
            ErrorMessage = "Price must be between 1.00 and 1000.00")]
        public string Price { get; set; }
        [Display(Name = "Tax Rate %")]
        [Range(0.00, 0.99,
            ErrorMessage = "Tax Rate must be between 0.00 and 0.99")]
        public string TaxRate { get; set; }
    }
}
