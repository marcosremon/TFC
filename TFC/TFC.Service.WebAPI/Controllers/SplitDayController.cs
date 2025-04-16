using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.SplitDay.ActualizarSplitDay;
using TFC.Application.DTO.SplitDay.AnyadirSplitDay;
using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.GetAllUserSplits;
using TFC.Application.Interface.Application;

namespace TFC.Service.WebApi.Controllers
{
    public class SplitDayController : Controller
    {
        private readonly ISplitDayApplication _splitDayApplication;

        public SplitDayController(ISplitDayApplication splitDayApplication)
        {
            _splitDayApplication = splitDayApplication;
        }

        [HttpPost("AddSplitDay")]
        public async Task<ActionResult<AddSplitDayResponse>> CreateSplitDay([FromBody] AddSplitDayRequest anyadirSplitDayRequest)
        {
            AddSplitDayResponse response = await _splitDayApplication.CreateSplitDay(anyadirSplitDayRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPut("UpdateSplitDay")]
        public async Task<ActionResult<ActualizarSplitDayResponse>> UpdateSplitDay([FromBody] ActualizarSplitDayRequest actualizarSplitDayRequest)
        {
            ActualizarSplitDayResponse response = await _splitDayApplication.UpdateSplitDay(actualizarSplitDayRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteSplitDay")]
        public async Task<ActionResult<DeleteSplitDayResponse>> DeleteSplitDay([FromBody] DeleteSplitDayRequest deleteSplitDayRequest)
        {
            DeleteSplitDayResponse response = await _splitDayApplication.DeleteSplitDay(deleteSplitDayRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPost("GetSplitsDay")]
        public async Task<ActionResult<GetAllUserSplitsResponse>> GetAllUserSplits([FromBody] GetAllUserSplitsRequest getAllUserSplitsResponse)
        {
            GetAllUserSplitsResponse response = await _splitDayApplication.GetAllUserSplits(getAllUserSplitsResponse);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}