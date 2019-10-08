using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    [Table("TblPurchaseOrders")]
    public class TblPurchaseOrder:LineId
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public string Budget { get; set; }
        public string DateRaised { get; set; }
        public string DateFullfilled { get; set; }
        public string DateRequired { get; set; }
        public string Note { get; set; }
        public string To { get; set; }
        public string ToEmail { get; set; }
        public string ToDetail { get; set; }
        public string DeliverTo { get; set; }
        public string DeliverToDetail { get; set; }
        public string InvoiceTo { get; set; }
        public string InvoiceToDetail { get; set; }
        public string Price { get; set; }
        public string Tax { get; set; }
        public string Total { get; set; }

        // navigation properties   
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<TblPurchaseOrderItem> Items { get; set; }
    }
}
