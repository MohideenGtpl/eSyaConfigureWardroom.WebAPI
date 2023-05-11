using ConfigureWardRoom.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigureWardRoom.IF
{
   public interface IRoomLocationRepository
    {
        Task<List<DO_RoomLocation>> GetActiveStores();

        Task<List<DO_RoomLocation>> GetRoomLocationbyBusinessKey(int businessKey);

        Task<DO_ReturnParameter> InsertIntoRoomLocation(DO_RoomLocation obj);

        Task<DO_ReturnParameter> UpdateRoomLocation(DO_RoomLocation obj);

        Task<DO_ReturnParameter> ActiveOrDeActiveRoomLocation(bool status, int businessKey, int locationId);
    }
}
