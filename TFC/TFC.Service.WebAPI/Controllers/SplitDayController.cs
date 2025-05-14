using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.SplitDay.AnyadirSplitDay;
using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.GetAllUserSplits;
using TFC.Application.DTO.SplitDay.UpdateSplitDay;
using TFC.Application.Interface.Application;

namespace TFC.Service.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SplitDayController : ControllerBase
    {
        private readonly ISplitDayApplication _splitDayApplication;

        public SplitDayController(ISplitDayApplication splitDayApplication)
        {
            _splitDayApplication = splitDayApplication;
        }

        [HttpPost("CreateSplitDay")]
        public async Task<ActionResult<AddSplitDayResponse>> CreateSplitDay([FromBody] AddSplitDayRequest anyadirSplitDayRequest)
        {
            try
            {
                AddSplitDayResponse response = await _splitDayApplication.CreateSplitDay(anyadirSplitDayRequest);
                if (response.IsSuccess)
                {
                    return Created(string.Empty, response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateSplitDay")]
        public async Task<ActionResult<UpdateSplitDayResponse>> UpdateSplitDay([FromBody] UpdateSplitDayRequest actualizarSplitDayRequest)
        {
            try
            {
                UpdateSplitDayResponse response = await _splitDayApplication.UpdateSplitDay(actualizarSplitDayRequest);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSplitDay")]
        public async Task<ActionResult<DeleteSplitDayResponse>> DeleteSplitDay([FromBody] DeleteSplitDayRequest deleteSplitDayRequest)
        {
            try
            {
                DeleteSplitDayResponse response = await _splitDayApplication.DeleteSplitDay(deleteSplitDayRequest);
                if (response.IsSuccess)
                {
                    return NoContent();
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetSplitsDay")]
        public async Task<ActionResult<GetAllUserSplitsResponse>> GetAllUserSplits([FromBody] GetAllUserSplitsRequest getAllUserSplitsRequest)
        {
            try
            {
                GetAllUserSplitsResponse response = await _splitDayApplication.GetAllUserSplits(getAllUserSplitsRequest);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}