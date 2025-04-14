using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.DeleteRoutine;
using TFC.Application.DTO.Routine.GetRoutines;
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
            if (createRoutineRequest == null 
                || string.IsNullOrEmpty(createRoutineRequest.RoutineName))
            {
                return BadRequest();
            }

            CreateRoutineResponse response = await _routineApplication.CreateRoutine(createRoutineRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("UpdateRoutine")]
        public async Task<ActionResult<UpdateRoutineResponse>> UpdateUser([FromBody] UpdateRoutineRequest updateRoutineRequest)
        {
            if (updateRoutineRequest == null 
                || updateRoutineRequest.RoutineId == null)
            {
                return BadRequest();
            }

            UpdateRoutineResponse response = await _routineApplication.UpdateUser(updateRoutineRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpDelete("DeleteRoutine")]
        public async Task<ActionResult<DeleteRoutineResponse>> DeleteUser([FromBody] DeleteRoutineRequest deleteRoutineRequest)
        {
            if (deleteRoutineRequest == null 
                || string.IsNullOrEmpty(deleteRoutineRequest.UserDni)
                || deleteRoutineRequest.RoutineId == null)
            {
                return BadRequest();
            }

            DeleteRoutineResponse response = await _routineApplication.DeleteRoutine(deleteRoutineRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}