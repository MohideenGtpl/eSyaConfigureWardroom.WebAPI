using System;
using System.Collections.Generic;

namespace ConfigureWardRoom.DL.Entities
{
    public partial class GtEswrbl
    {
        public int BusinessKey { get; set; }
        public int WardId { get; set; }
        public int RoomId { get; set; }
        public int LocationId { get; set; }
        public int NoOfBeds { get; set; }
        public int OccupiedBeds { get; set; }
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
