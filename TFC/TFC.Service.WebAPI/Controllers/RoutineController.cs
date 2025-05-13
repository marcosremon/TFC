using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetRoutines;
using TFC.Application.DTO.Routine.GetRoutinesByFriendCode;
using TFC.Application.Interface.Application;

namespace TFC.Service.WebApi.Controllers
{
    public class RoutineController : Controller
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
                    return Created(string.Empty, response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
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
                    return Ok(response);
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
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
                    return NoContent();
                }

                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetRoutinesByFriendCode")]
        public async Task<ActionResult<GetRoutinesByFriendCodeResponse>> GetRoutinesByFriendCode([FromBody] GetRoutinesByFriendCodeRequest getRoutinesByFriendCodeRequest)
        {
            try
            {
                GetRoutinesByFriendCodeResponse getRoutinesByFriendCodeResponse = await _routineApplication.GetRoutinesByFriendCode(getRoutinesByFriendCodeRequest);
                if (getRoutinesByFriendCodeResponse.IsSuccess)
                {
                    return Ok(getRoutinesByFriendCodeResponse);
                }

                return BadRequest(getRoutinesByFriendCodeResponse?.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}