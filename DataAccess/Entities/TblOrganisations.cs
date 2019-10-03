using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    [Table("TblOrganisations")]
    public class TblOrganisation:LineId
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        // navigation properties
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<TblOrganisationItem> Items { get; set; }
    }
}
