using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigureWardRoom.DL.Repository;
using ConfigureWardRoom.DO;
using ConfigureWardRoom.IF;
using Microsoft.AspNetCore.Mvc;

namespace ConfigureWardRoom.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomMasterController : ControllerBase
    {
        private readonly IRoomMasterRepository _IRoomMasterRepository;

        public RoomMasterController(IRoomMasterRepository IRoomMasterRepository)
        {
            _IRoomMasterRepository = IRoomMasterRepository;
        }

        #region RoomMaster
        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rms = await _IRoomMasterRepository.GetAllRooms();
            return Ok(rms);
        }
        [HttpPost]
        public async Task<IActionResult> InsertIntoRoomMaster(DO_RoomMaster obj)
        {
            var msg = await _IRoomMasterRepository.InsertIntoRoomMaster(obj);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRoomMaster(DO_RoomMaster obj)
        {
            var msg = await _IRoomMasterRepository.UpdateRoomMaster(obj);
            return Ok(msg);
        }
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveRoomMaster(bool status, int roomId)
        {
            var msg = await _IRoomMasterRepository.ActiveOrDeActiveRoomMaster(status, roomId);
            return Ok(msg);
        }
        #endregion
    }
}