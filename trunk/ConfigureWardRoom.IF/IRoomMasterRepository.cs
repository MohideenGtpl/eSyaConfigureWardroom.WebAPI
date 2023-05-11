using ConfigureWardRoom.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigureWardRoom.IF
{
   public interface IRoomMasterRepository
    {
        Task<List<DO_RoomMaster>> GetAllRooms();

        Task<DO_ReturnParameter> InsertIntoRoomMaster(DO_RoomMaster obj);

        Task<DO_ReturnParameter> UpdateRoomMaster(DO_RoomMaster obj);

        Task<DO_ReturnParameter> ActiveOrDeActiveRoomMaster(bool status, int roomId);
    }
}
