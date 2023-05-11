using System;
using System.Collections.Generic;

namespace ConfigureWardRoom.DL.Entities
{
    public partial class GtEswrlc
    {
        public int BusinessKey { get; set; }
        public int LocationId { get; set; }
        public string LocationDesc { get; set; }
        public string MobileNumber { get; set; }
        public int StoreCode { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }
    }
}
