using System;
using System.Collections.Generic;
using System.Text;

namespace Models.JsonModels
{
    public class PurchaseOrderItemJsonModel
    {
        public string DBLineId { get; set; }
        public string id { get; set; }
        public string RVN { get; set; }
        public string taxcode { get; set; }
        public string brand { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string quantity { get; set; }
        public string tax { get; set; }
        public string total { get; set; }
    }
    public class PurchaseOrderJsonModel
    {
        public string PurchaseOrderid { get; set; }
        public string ApplicationUserid { get; set; }
        public string Organisationid { get; set; }
        public string RowVersionNo { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string DateRaised { get; set; }
        public string DateRequiredBy { get; set; }
        public string Note { get; set; }
        public string Price { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }
        public string BudgetCode { get; set; }
        public string To { get; set; }
        public string ToEmail { get; set; }
        public string ToPerson { get; set; }
        public string DeliverTo { get; set; }
        public string InvoiceTo { get; set; }
        public string ToDetail { get; set; }
        public string DeliverToDetail { get; set; }
        public string InvoiceToDetail { get; set; }

        public List<PurchaseOrderItemJsonModel> LineItem = new List<PurchaseOrderItemJsonModel>();
    }
}
