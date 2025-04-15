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
            if (anyadirSplitDayRequest == null
                || anyadirSplitDayRequest.DayName == null
                || anyadirSplitDayRequest.UserId == null
                || anyadirSplitDayRequest.RoutineId == null)
            {
                return BadRequest();
            }

            AddSplitDayResponse response = await _splitDayApplication.CreateSplitDay(anyadirSplitDayRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("UpdateSplitDay")]
        public async Task<ActionResult<ActualizarSplitDayResponse>> UpdateSplitDay([FromBody] ActualizarSplitDayRequest actualizarSplitDayRequest)
        {
            if (actualizarSplitDayRequest == null
               || actualizarSplitDayRequest.DayName == null
               || actualizarSplitDayRequest.UserId == null
               || actualizarSplitDayRequest.RoutineId == null)
            {
                return BadRequest();
            }

            ActualizarSplitDayResponse response = await _splitDayApplication.UpdateSplitDay(actualizarSplitDayRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpDelete("DeleteSplitDay")]
        public async Task<ActionResult<DeleteSplitDayResponse>> DeleteSplitDay([FromBody] DeleteSplitDayRequest deleteSplitDayRequest)
        {
            if (deleteSplitDayRequest == null
                || deleteSplitDayRequest.DayName == null
                || deleteSplitDayRequest.UserId == null
                || deleteSplitDayRequest.RoutineId == null)
            {
                return BadRequest();
            }

            DeleteSplitDayResponse response = await _splitDayApplication.DeleteSplitDay(deleteSplitDayRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPost("GetSplitsDay")]
        public async Task<ActionResult<GetAllUserSplitsResponse>> GetAllUserSplits([FromBody] GetAllUserSplitsRequest getAllUserSplitsResponse)
        {
            GetAllUserSplitsResponse response = await _splitDayApplication.GetAllUserSplits(getAllUserSplitsResponse);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}