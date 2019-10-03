using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{

    [Table("Lookups")]
    public class TblLookup : LineId
    {
        // contains values to populate dropdown lists or system values 
        // table columns        
        public string Lookup { get; set; }
        public string Value { get; set; }
        public string Note { get; set; }
    }
}
