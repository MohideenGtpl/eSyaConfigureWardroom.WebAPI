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
    public class WardRoomController : ControllerBase
    {
        private readonly IWardRoomLinkRepository _IWardRoomLinkRepository;

        public WardRoomController(IWardRoomLinkRepository IWardRoomLinkRepository)
        {
            _IWardRoomLinkRepository = IWardRoomLinkRepository;
        }

        #region Ward Room Location Link

        [HttpGet]
        public async Task<IActionResult> GetActiveLocations()
        {
            var wrds = await _IWardRoomLinkRepository.GetActiveLocations();
            return Ok(wrds);
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveWards()
        {
            var wrds = await _IWardRoomLinkRepository.GetActiveWards();
            return Ok(wrds);
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveRooms()
        {
            var wrds = await _IWardRoomLinkRepository.GetActiveRooms();
            return Ok(wrds);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoadGridWardRoomLinksbyBusinessKey(int businesskey)
        {
            var rloc = await _IWardRoomLinkRepository.GetLoadGridWardRoomLinksbyBusinessKey(businesskey);
            return Ok(rloc);
        }

        [HttpGet]
        public async Task<IActionResult> GetWardRoomLink(int businesskey, int roomId, int wardId, int locationId)
        {
            var rloc = await _IWardRoomLinkRepository.GetWardRoomLink(businesskey, roomId, wardId, locationId);
            return Ok(rloc);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdatetWardRoomLink(DO_WardRoomLink obj)
        {
            var msg = await _IWardRoomLinkRepository.AddOrUpdatetWardRoomLink(obj);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> ActiveOrDeActiveWardRoomLink(DO_WardRoomLink obj)
        {
            var msg = await _IWardRoomLinkRepository.ActiveOrDeActiveWardRoomLink(obj);
            return Ok(msg);
        }
        
        
        #endregion
    }
}