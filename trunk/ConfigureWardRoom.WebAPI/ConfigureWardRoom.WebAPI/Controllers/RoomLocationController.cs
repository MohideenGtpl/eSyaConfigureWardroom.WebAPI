using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigureWardRoom. DL.Repository;
using ConfigureWardRoom.DO;
using ConfigureWardRoom.IF;
using Microsoft.AspNetCore.Mvc;

namespace ConfigureWardRoom.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomLocationController : ControllerBase
    {
        private readonly IRoomLocationRepository _IRoomLocationRepository;
        public RoomLocationController(IRoomLocationRepository IRoomLocationRepository)
        {
            _IRoomLocationRepository = IRoomLocationRepository;
        }

        #region Room Location
        [HttpGet]
        public async Task<IActionResult> GetActiveStores()
        {
            var st = await _IRoomLocationRepository.GetActiveStores();
            return Ok(st);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoomLocationbyBusinessKey(int businessKey)
        {
            var rloc = await _IRoomLocationRepository.GetRoomLocationbyBusinessKey(businessKey);
            return Ok(rloc);
        }
        [HttpPost]
        public async Task<IActionResult> InsertIntoRoomLocation(DO_RoomLocation obj)
        {
            var msg = await _IRoomLocationRepository.InsertIntoRoomLocation(obj);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRoomLocation(DO_RoomLocation obj)
        {
            var msg = await _IRoomLocationRepository.UpdateRoomLocation(obj);
            return Ok(msg);
        }
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveRoomLocation(bool status, int businessKey, int locationId)
        {
            var msg = await _IRoomLocationRepository.ActiveOrDeActiveRoomLocation(status, businessKey, locationId);
            return Ok(msg);
        }
        #endregion
    }
}