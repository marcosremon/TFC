using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Exercise.AddExercise;
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
    }
}
