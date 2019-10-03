using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.ViewModels
{
    public class PurchaseOrderIndexItem
    {
        //hidden fields
        public string PurchaseOrderID { get; set; }
        public string ApplicationUserid { get; set; }
        // display fields
        [Display(Name = "Code")]
        public string Code { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }       
        [Display(Name = "Date Raised")]
        public string DateRaised { get; set; }
        [Display(Name = "Date Required By")]
        public string DateRequiredBy { get; set; }
        [Display(Name = "Value")]
        public string Value { get; set; }
    }

    public class PurchaseOrderIndexViewModel
    {
        public string OrderBy { get; set; }
        public string Order { get; set; }
        public List<PurchaseOrderIndexItem> Index = new List<PurchaseOrderIndexItem>();
    }
}
