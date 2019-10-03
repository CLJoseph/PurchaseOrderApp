using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Entities
{
    public class LineId
    {
        // this is the line identifier (primary Key) in a table
        // using a Guid as this lifts the responsibility of generating
        // this value from the database.   
        public Guid ID { get; set; }
        // row version control
        // each time a line is modified the database will record that time.
        // useful for checking that data has not changed between a read and write
        [Timestamp]
        public byte[] RowVersionNo { get; set; }
    }
}
