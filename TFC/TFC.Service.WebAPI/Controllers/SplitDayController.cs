using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.SplitDay.AnyadirSplitDay;
using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.GetAllUserSplits;
using TFC.Application.DTO.SplitDay.UpdateSplitDay;
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
            try
            {
                AddSplitDayResponse addSplitDayResponse = await _splitDayApplication.CreateSplitDay(anyadirSplitDayRequest);
                if (addSplitDayResponse.IsSuccess)
                {
                    return Ok(addSplitDayResponse);
                }

                return BadRequest(addSplitDayResponse.Message);
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
                UpdateSplitDayResponse updateSplitDayResponse = await _splitDayApplication.UpdateSplitDay(actualizarSplitDayRequest);
                if (updateSplitDayResponse.IsSuccess)
                {
                    return Ok(updateSplitDayResponse);
                }

                return BadRequest(updateSplitDayResponse.Message);
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
                DeleteSplitDayResponse deletesplitdayresponse = await _splitDayApplication.DeleteSplitDay(deleteSplitDayRequest);
                if (deletesplitdayresponse.IsSuccess)
                {
                    return Ok(deletesplitdayresponse);
                }

                return BadRequest(deletesplitdayresponse.Message);
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
                GetAllUserSplitsResponse getallusersplitsresponse = await _splitDayApplication.GetAllUserSplits(getAllUserSplitsRequest);
                if (getallusersplitsresponse.IsSuccess)
                {
                    return Ok(getallusersplitsresponse);
                }

                return BadRequest(getallusersplitsresponse.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}