using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.SplitDay.AnyadirSplitDay;
using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.GetAllUserSplits;
using TFC.Application.DTO.SplitDay.UpdateSplitDay;
using TFC.Application.Interface.Application;
using TFC.Transversal.Logs;

namespace TFC.Service.WebApi.Controllers
{
    [ApiController]
    [Route("api/split-day")]
    public class SplitDayController : ControllerBase
    {
        private readonly ISplitDayApplication _splitDayApplication;

        public SplitDayController(ISplitDayApplication splitDayApplication)
        {
            _splitDayApplication = splitDayApplication;
        }

        [HttpPost("create-split-day")]
        public async Task<ActionResult<AddSplitDayResponse>> CreateSplitDay([FromBody] AddSplitDayRequest anyadirSplitDayRequest)
        {
            try
            {
                AddSplitDayResponse response = await _splitDayApplication.CreateSplitDay(anyadirSplitDayRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"SplitDay añadido correctamente al usuario con id: {response.UserDTO?.UserId}");
                    return Created(string.Empty, response);
                }

                Log.Instance.Trace($"Error al añadir el SplitDay: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"CreateSplitDay --> Error al añadir el SplitDay: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-split-day")]
        public async Task<ActionResult<UpdateSplitDayResponse>> UpdateSplitDay([FromBody] UpdateSplitDayRequest actualizarSplitDayRequest)
        {
            try
            {
                UpdateSplitDayResponse response = await _splitDayApplication.UpdateSplitDay(actualizarSplitDayRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"SplitDay actualizado correctamente para el usuario con id: {response.UserDTO?.UserId}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al actualizar el SplitDay: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"UpdateSplitDay --> Error al actualizar el SplitDay: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-split-day")]
        public async Task<ActionResult<DeleteSplitDayResponse>> DeleteSplitDay([FromBody] DeleteSplitDayRequest deleteSplitDayRequest)
        {
            try
            {
                DeleteSplitDayResponse response = await _splitDayApplication.DeleteSplitDay(deleteSplitDayRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"SplitDay eliminado correctamente");
                    return NoContent();
                }

                Log.Instance.Trace($"Error al eliminar el SplitDay: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"DeleteSplitDay --> Error al eliminar el SplitDay: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("get-splits-day")]
        public async Task<ActionResult<GetAllUserSplitsResponse>> GetAllUserSplits([FromBody] GetAllUserSplitsRequest getAllUserSplitsRequest)
        {
            try
            {
                GetAllUserSplitsResponse response = await _splitDayApplication.GetAllUserSplits(getAllUserSplitsRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"SplitDay obtenidos correctamente");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al obtener los SplitDay: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"GetAllUserSplits --> Error al obtener los SplitDay: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}