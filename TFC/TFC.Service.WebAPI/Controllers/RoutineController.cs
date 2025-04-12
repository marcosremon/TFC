using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Routine.CreateRoutine;
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

        //[HttpPut("UpdateUser")]
        //public async Task<ActionResult<UpdateUserResponse>> UpdateUser([FromBody] UpdateUserRequst updateUserRequest)
        //{
        //    if (updateUserRequest == null || string.IsNullOrEmpty(updateUserRequest.DniToBeFound) || string.IsNullOrEmpty(updateUserRequest.Email))
        //    {
        //        return BadRequest();
        //    }

        //    UpdateUserResponse updateUserResponse = await _userApplication.UpdateUser(updateUserRequest);
        //    if (updateUserResponse.IsSuccess)
        //    {
        //        return Ok(updateUserResponse);
        //    }

        //    return BadRequest(updateUserResponse.Message);
        //}

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