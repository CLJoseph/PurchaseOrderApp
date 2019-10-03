using System;
using System.Collections.Generic;
using System.Text;

namespace Models.ViewModels
{
    public class PurchaseOrderItemViewModel
    {
        public string Id { get; set; }
        public byte [] RowVersion { get; set; }
        public string Brand { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string TaxCode { get; set; }
        public string Quantity { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }       
    }
}
