using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetRoutines;
using TFC.Application.DTO.Routine.GetRoutinesByFriendCode;
using TFC.Application.DTO.User.DeleteUser;
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
        public async Task<ActionResult<CreateRoutineResponse>> CreateUser([FromBody] CreateRoutineRequest createRoutineRequest)
        {
            CreateRoutineResponse response = await _routineApplication.CreateRoutine(createRoutineRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPut("UpdateRoutine")]
        public async Task<ActionResult<UpdateRoutineResponse>> UpdateUser([FromBody] UpdateRoutineRequest updateRoutineRequest)
        {
            UpdateRoutineResponse response = await _routineApplication.UpdateUser(updateRoutineRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteRoutine")]
        public async Task<ActionResult<DeleteRoutineResponse>> DeleteUser([FromBody] DeleteRoutineRequest deleteRoutineRequest)
        {
            DeleteRoutineResponse response = await _routineApplication.DeleteRoutine(deleteRoutineRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPost("GetRoutinesByFriendCode")]
        public async Task<ActionResult<GetRoutinesByFriendCodeResponse>> GetRoutinesByFriendCode([FromBody] GetRoutinesByFriendCodeRequest getRoutinesByFriendCodeRequest)
        {
            GetRoutinesByFriendCodeResponse response = await _routineApplication.GetRoutinesByFriendCode(getRoutinesByFriendCodeRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}