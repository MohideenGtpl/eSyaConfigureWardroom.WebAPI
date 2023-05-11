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
    public class WardMasterController : ControllerBase
    {
        private readonly IWardMasterRepository _IWardMasterRepository;
        public WardMasterController(IWardMasterRepository IWardMasterRepository)
        {
            _IWardMasterRepository = IWardMasterRepository;
        }

        #region WardMaster
        
        [HttpGet]
        public async Task<IActionResult> GetAllWards()
        {
            var wrds = await _IWardMasterRepository.GetAllWards();
            return Ok(wrds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertIntoWardMaster(DO_WardMaster obj)
        {
            var msg = await _IWardMasterRepository.InsertIntoWardMaster(obj);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateWardMaster(DO_WardMaster obj)
        {
            var msg = await _IWardMasterRepository.UpdateWardMaster(obj);
            return Ok(msg);
        }
        [HttpGet]
        public async Task<IActionResult> ActiveOrDeActiveWardMaster(bool status, int wardId)
        {
            var msg = await _IWardMasterRepository.ActiveOrDeActiveWardMaster(status, wardId);
            return Ok(msg);
        }
        #endregion
    }
}