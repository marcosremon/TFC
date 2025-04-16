using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Exercise.AddExercise;
using TFC.Application.DTO.Exercise.GetExercisesByDayName;
using TFC.Application.DTO.Exercise.UpdateExercise;
using TFC.Application.Interface.Application;

namespace TFC.Service.WebApi.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IExerciseApplication _exerciseApplication;

        public ExerciseController(IExerciseApplication exerciseApplication)
        {
            _exerciseApplication = exerciseApplication;
        }

        [HttpPost("AddExercise")]
        public async Task<ActionResult<AddExerciseResponse>> AddExercise([FromBody] AddExerciseRequest addExerciseRequest)
        {
            if (addExerciseRequest == null
                || addExerciseRequest.UserId == null
                || addExerciseRequest.RoutineId == null
                || addExerciseRequest.DayName == null
                || string.IsNullOrEmpty(addExerciseRequest.ExerciseName))
            {
                return BadRequest();
            }

            AddExerciseResponse response = await _exerciseApplication.AddExercise(addExerciseRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPut("UpdateExercise")]
        public async Task<ActionResult<UpdateExerciseResponse>> UpdateExercise([FromBody] UpdateExerciseRequest updateExerciseRequest)
        {
            if (updateExerciseRequest == null
                || updateExerciseRequest.UserId == null
                || updateExerciseRequest.RoutineId == null
                || updateExerciseRequest.DayName == null
                || string.IsNullOrEmpty(updateExerciseRequest.ExerciseName))
            {
                return BadRequest();
            }

            UpdateExerciseResponse response = await _exerciseApplication.UpdateExercise(updateExerciseRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpDelete("DeleteExercise")]
        public async Task<ActionResult<DeleteExerciseResponse>> DeleteExercise([FromBody] DeleteExerciseRequest deleteExerciseRequest)
        {
            if (deleteExerciseRequest == null
                || deleteExerciseRequest.UserId == null
                || deleteExerciseRequest.RoutineId == null
                || deleteExerciseRequest.DayName == null
                || string.IsNullOrEmpty(deleteExerciseRequest.ExerciseName))
            {
                return BadRequest();
            }

            DeleteExerciseResponse response = await _exerciseApplication.DeleteExercise(deleteExerciseRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPost("GetExercisesByDayName")]
        public async Task<ActionResult<GetExercisesByDayNameResponse>> GetExercisesByDayName([FromBody] GetExercisesByDayNameRequest getExercisesByDayNameRequest)
        {
            if (getExercisesByDayNameRequest == null
                || getExercisesByDayNameRequest.UserId == null
                || getExercisesByDayNameRequest.RoutineId == null
                || getExercisesByDayNameRequest.DayName == null)
            {
                return BadRequest();
            }

            GetExercisesByDayNameResponse response = await _exerciseApplication.GetExercisesByDayName(getExercisesByDayNameRequest);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}
