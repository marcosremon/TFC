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
            AddExerciseResponse response = await _exerciseApplication.AddExercise(addExerciseRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPut("UpdateExercise")]
        public async Task<ActionResult<UpdateExerciseResponse>> UpdateExercise([FromBody] UpdateExerciseRequest updateExerciseRequest)
        {
            UpdateExerciseResponse response = await _exerciseApplication.UpdateExercise(updateExerciseRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpDelete("DeleteExercise")]
        public async Task<ActionResult<DeleteExerciseResponse>> DeleteExercise([FromBody] DeleteExerciseRequest deleteExerciseRequest)
        {
            DeleteExerciseResponse response = await _exerciseApplication.DeleteExercise(deleteExerciseRequest);
            if (!response.IsSuccess || response == null)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }

        [HttpPost("GetExercisesByDayName")]
        public async Task<ActionResult<GetExercisesByDayNameResponse>> GetExercisesByDayName([FromBody] GetExercisesByDayNameRequest getExercisesByDayNameRequest)
        {
            GetExercisesByDayNameResponse getExercisesByDayNameResponse = await _exerciseApplication.GetExercisesByDayName(getExercisesByDayNameRequest);
            if (getExercisesByDayNameResponse.IsSuccess)
            {
                return Ok(getExercisesByDayNameResponse);
            }

            return BadRequest(getExercisesByDayNameResponse.Message);
        }
    }
}
