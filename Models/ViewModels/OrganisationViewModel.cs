using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels
{
    public class OrganisationViewModel
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
                      

        [Display(Name = "Organisation Name")]
        [Required(ErrorMessage = "Organisation Name is required")]
        public string Name { get; set; }
        [Display(Name = "Organisation Contact")]
        [Required(ErrorMessage = "Organisation Contact is required")]
        public string Contact { get; set; }

        [Display(Name = "Organisation Contact email")]
        [Required(ErrorMessage = "Organisation Contact email is required")]
        public string ContactEmail { get; set; }

        [Display(Name = "Organisation Contact No")]
        [Required(ErrorMessage = "Mobile no. is required")]
      

        public string ContactNo { get; set; }

        public string Address { get; set; }


        [Display(Name = "Line 1")]
        [Required(ErrorMessage = "First line of address is required")]
        public string Line01 { get; set; }
        [Display(Name = "Line 2")]    
        public string Line02 { get; set; }
        [Display(Name = "Line 3")]
        public string Line03 { get; set; }
        [Display(Name = "Line 4")]
        public string Line04 { get; set; }
        [Display(Name = "Line 5")]
        public string Line05 { get; set; }
        [Display(Name = "Code")]
        public string Code { get; set; }
    }
}
