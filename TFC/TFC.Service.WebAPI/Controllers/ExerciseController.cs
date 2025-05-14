using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Exercise.AddExercise;
using TFC.Application.DTO.Exercise.GetExercisesByDayName;
using TFC.Application.DTO.Exercise.UpdateExercise;
using TFC.Application.Interface.Application;

namespace TFC.Service.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseApplication _exerciseApplication;

        public ExerciseController(IExerciseApplication exerciseApplication)
        {
            _exerciseApplication = exerciseApplication;
        }

        [HttpPost("AddExercise")]
        public async Task<ActionResult<AddExerciseResponse>> AddExercise([FromBody] AddExerciseRequest addExerciseRequest)
        {
            try
            {
                AddExerciseResponse response = await _exerciseApplication.AddExercise(addExerciseRequest);
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

        [HttpPut("UpdateExercise")]
        public async Task<ActionResult<UpdateExerciseResponse>> UpdateExercise([FromBody] UpdateExerciseRequest updateExerciseRequest)
        {
            try
            {
                UpdateExerciseResponse response = await _exerciseApplication.UpdateExercise(updateExerciseRequest);
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

        [HttpDelete("DeleteExercise")]
        public async Task<ActionResult<DeleteExerciseResponse>> DeleteExercise([FromBody] DeleteExerciseRequest deleteExerciseRequest)
        {
            try
            {
                DeleteExerciseResponse response = await _exerciseApplication.DeleteExercise(deleteExerciseRequest);
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

        [HttpPost("GetExercisesByDayName")]
        public async Task<ActionResult<GetExercisesByDayNameResponse>> GetExercisesByDayName([FromBody] GetExercisesByDayNameRequest getExercisesByDayNameRequest)
        {
            try
            {
                GetExercisesByDayNameResponse getExercisesByDayNameResponse = await _exerciseApplication.GetExercisesByDayName(getExercisesByDayNameRequest);
                if (getExercisesByDayNameResponse.IsSuccess)
                {
                    // 200 OK is appropriate for successful retrieval
                    return Ok(getExercisesByDayNameResponse);
                }

                return BadRequest(getExercisesByDayNameResponse.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
