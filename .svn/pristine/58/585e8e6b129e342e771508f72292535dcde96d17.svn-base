using ConfigureWardRoom.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigureWardRoom.IF
{
    public interface IWardRoomLinkRepository
    {
        Task<List<DO_RoomLocation>> GetActiveLocations();

        Task<List<DO_WardMaster>> GetActiveWards();

        Task<List<DO_RoomMaster>> GetActiveRooms();

        Task<List<DO_WardRoomLink>> GetLoadGridWardRoomLinksbyBusinessKey(int businesskey);

        Task<DO_WardRoomLink> GetWardRoomLink(int businesskey, int roomId, int wardId, int locationId);

        Task<DO_ReturnParameter> AddOrUpdatetWardRoomLink(DO_WardRoomLink obj);

        Task<DO_ReturnParameter> ActiveOrDeActiveWardRoomLink(DO_WardRoomLink obj);
    }
}
