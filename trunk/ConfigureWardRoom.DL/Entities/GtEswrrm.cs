using System;
using System.Collections.Generic;

namespace ConfigureWardRoom.DL.Entities
{
    public partial class GtEswrrm
    {
        public int RoomId { get; set; }
        public string Gender { get; set; }
        public string RoomShortDesc { get; set; }
        public string RoomDesc { get; set; }
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
