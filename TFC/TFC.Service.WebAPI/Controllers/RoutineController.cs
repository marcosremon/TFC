using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetRoutines;
using TFC.Application.DTO.Routine.GetRoutinesByFriendCode;
using TFC.Application.Interface.Application;
using TFC.Transversal.Logs;

namespace TFC.Service.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutineController : ControllerBase
    {
        private readonly IRoutineApplication _routineApplication;

        public RoutineController(IRoutineApplication routineApplication)
        {
            _routineApplication = routineApplication;
        }

        [HttpPost("CreateRoutine")]
        public async Task<ActionResult<CreateRoutineResponse>> CreateRoutine([FromBody] CreateRoutineRequest createRoutineRequest)
        {
            try
            {
                CreateRoutineResponse response = await _routineApplication.CreateRoutine(createRoutineRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"Rutina añadida correctamente al usuario con DNI: {createRoutineRequest.UserDni}");
                    return Created(string.Empty, response);
                }

                Log.Instance.Trace($"Error al añadir la rutina: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"CreateRoutine --> Error al añadir la rutina: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateRoutine")]
        public async Task<ActionResult<UpdateRoutineResponse>> UpdateRoutine([FromBody] UpdateRoutineRequest updateRoutineRequest)
        {
            try
            {
                UpdateRoutineResponse response = await _routineApplication.UpdateUser(updateRoutineRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"Rutina con nombre: {updateRoutineRequest.RoutineName} actualizada correctamente");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al actualizar la rutina: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"UpdateRoutine --> Error al actualizar la rutina: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRoutine")]
        public async Task<ActionResult<DeleteRoutineResponse>> DeleteRoutine([FromBody] DeleteRoutineRequest deleteRoutineRequest)
        {
            try
            {
                DeleteRoutineResponse response = await _routineApplication.DeleteRoutine(deleteRoutineRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"Rutina con id: {deleteRoutineRequest.RoutineId} eliminada correctamente");
                    return NoContent();
                }

                Log.Instance.Trace($"Error al eliminar la rutina: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"DeleteRoutine --> Error al eliminar la rutina: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetRoutinesByFriendCode")]
        public async Task<ActionResult<GetRoutinesByFriendCodeResponse>> GetRoutinesByFriendCode([FromBody] GetRoutinesByFriendCodeRequest getRoutinesByFriendCodeRequest)
        {
            try
            {
                GetRoutinesByFriendCodeResponse response = await _routineApplication.GetRoutinesByFriendCode(getRoutinesByFriendCodeRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"Rutinas del amigo con codigo: {getRoutinesByFriendCodeRequest.FriendCode} obtenidas correctamente");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al obtener las rutinas: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"GetRoutinesByFriendCode --> Error al obtener las rutinas: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}