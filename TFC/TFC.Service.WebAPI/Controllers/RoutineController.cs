using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TFC.Application.DTO.Routine.CreateRoutine;
using TFC.Application.DTO.Routine.GetRoutines;
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
            if (createRoutineRequest == null || string.IsNullOrEmpty(createRoutineRequest.RoutineName))
            {
                return BadRequest();
            }

            CreateRoutineResponse createRoutineResponse = await _routineApplication.CreateRoutine(createRoutineRequest);
            if (createRoutineResponse.IsSuccess)
            {
                return Ok(createRoutineResponse);
            }

            return BadRequest(createRoutineResponse.Message);
        }

        [HttpPut("UpdateRoutine")]
        public async Task<ActionResult<UpdateRoutineResponse>> UpdateUser([FromBody] UpdateRoutineRequest updateRoutineRequest)
        {
            if (updateRoutineRequest == null || string.IsNullOrEmpty(updateRoutineRequest.UserDni))
            {
                return BadRequest();
            }

            UpdateRoutineResponse updateRoutineResponse = await _routineApplication.UpdateUser(updateRoutineRequest);
            if (updateRoutineResponse.IsSuccess)
            {
                return Ok(updateRoutineResponse);
            }

            return BadRequest(updateRoutineResponse.Message);
        }

        //[HttpDelete("DeleteUser")]
        //public async Task<ActionResult<DeleteUserResponse>> DeleteUser([FromBody] DeleteUserRequest deleteUserRequest)
        //{
        //    if (deleteUserRequest == null || string.IsNullOrEmpty(deleteUserRequest.Dni))
        //    {
        //        return BadRequest();
        //    }

        //    DeleteUserResponse deleteUsersResponse = await _userApplication.DeleteUser(deleteUserRequest);
        //    if (deleteUsersResponse.IsSuccess)
        //    {
        //        return Ok(deleteUsersResponse);
        //    }

        //    return BadRequest(deleteUsersResponse.Message);
        //}
    }
}