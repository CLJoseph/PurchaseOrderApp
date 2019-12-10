using DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public ICollection<TblPurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<TblOrganisation> Organisations { get; set; }
        public string PersonName { get; set; }
        public string Organisation { get; set; }
        public string Address { get; set; }
    }
}
