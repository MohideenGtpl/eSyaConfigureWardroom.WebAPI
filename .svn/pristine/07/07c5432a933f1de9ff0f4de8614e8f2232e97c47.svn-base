using ConfigureWardRoom.DO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigureWardRoom.IF
{
    public interface IWardMasterRepository
    {
        Task<List<DO_WardMaster>> GetAllWards();

        Task<DO_ReturnParameter> InsertIntoWardMaster(DO_WardMaster obj);

        Task<DO_ReturnParameter> UpdateWardMaster(DO_WardMaster obj);

        Task<DO_ReturnParameter> ActiveOrDeActiveWardMaster(bool status, int wardId);
    }
}
