using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigureWardRoom.DO
{
    public class DO_RoomLocation
    {
        public int BusinessKey { get; set; }
        public int LocationId { get; set; }
        public string LocationDesc { get; set; }
        public string MobileNumber { get; set; }
        public int StoreCode { get; set; }
        public bool ActiveStatus { get; set; }
        public string FormId { get; set; }
        public int UserID { get; set; }
        public string TerminalID { get; set; }
        public string StoreDesc { get; set; }

    }
}
