using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace Models.ViewModels
{
    public class PurchaseOrderViewModel
    {
        // hidden 
        public string ApplicationUserId { get; set; }
        public string Id { get; set; }
        public byte[] RowVersionNo { get; set; }
        // data entry/edit fields
        public string Code { get; set; }
        public string Status { get; set; }
        public string Budget { get; set; }
        public List<string> BudgetCodes { get; set; }
        public string DateRaised { get; set; }
        public string DateFullfilled { get; set; }
        public string DateRequired { get; set; }
        public string Note { get; set; }
        public string FromDetail { get; set; }
        public string To { get; set; }
        public string ToEmail { get; set; }
        public string ToPerson { get; set; }
        public string ToDetail { get; set; }
        public List<OrgDetail> ToOptions { get; set; }
        public string DeliverTo { get; set; }
        public string DeliverToDetail { get; set; }
        public List<OrgDetail> DeliverToOptions { get; set; }
        public string InvoiceTo { get; set; }
        public string InvoiceToDetail { get; set; }
        public List<OrgDetail> InvoiceToOptions { get; set; }
        public string Price { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }
        public List<SelectListItem> HTMLSelectOrganisationItems = new List<SelectListItem>();
        public List<PurchaseOrderItemViewModel> Items = new List<PurchaseOrderItemViewModel>();
    }
}
