using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    [Table("PurchaseOrderItem")]
    public class TblPurchaseOrderItem:LineId
    {
        public string Brand { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string TaxCode { get; set; }
        public string Quantity { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }
        public TblPurchaseOrder PurchaseOrder { get; set; }
    }
}
