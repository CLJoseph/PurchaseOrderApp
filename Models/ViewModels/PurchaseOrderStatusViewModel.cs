using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Text;


namespace Models.ViewModels
{
    public class PurchaseOrderStatusViewModel
    {
        // hidden 
        public string ApplicationUserId { get; set; }
        public string Id { get; set; }   
        public string Code { get; set; }
        public string Status { get; set; }
        public List<string> NewStatus = new List<string>(); 
        public string Budget { get; set; }     
        public string DateRaised { get; set; }
        public string DateFullfilled { get; set; }
        public string DateRequired { get; set; }
        public string Note { get; set; }
        public string ToDetail { get; set; }
        public string DeliverToDetail { get; set; }
        public string InvoiceToDetail { get; set; }

        public string Price { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }
    }
}
