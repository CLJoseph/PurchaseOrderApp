using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    [Table("TblOrganisationItems")]
    public class TblOrganisationItem:LineId
    { 
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string TaxRate { get; set; }
        public Guid TblOrganisationId { get; set; }
    }
}
