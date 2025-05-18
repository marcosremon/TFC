using Microsoft.AspNetCore.Mvc;
using TFC.Application.DTO.Exercise.AddExercise;
using TFC.Application.DTO.Exercise.GetExercisesByDayName;
using TFC.Application.DTO.Exercise.UpdateExercise;
using TFC.Application.Interface.Application;
using TFC.Transversal.Logs;

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
                    Log.Instance.Trace($"Ejercicio añadido correctamente al usuario con id: {response.UserDTO?.UserId}");
                    return Created(string.Empty, response);
                }

                Log.Instance.Trace($"Error al añadir el ejercicio: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"AddExercise --> Error al añadir el ejercicio: {ex.Message}");
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
                    Log.Instance.Trace($"Ejercicio actualizado correctamente para el usuario con id: {response.UserDTO?.UserId}");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al actualizar el ejercicio: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"UpdateExercise --> Error al actualizar el ejercicio: {ex.Message}");
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
                    Log.Instance.Trace($"Ejercicio eliminado correctamente para el usuario con id: {response.UserDTO?.UserId}");
                    return NoContent();
                }

                Log.Instance.Trace($"Error al eliminar el ejercicio: {response?.Message}");
                return BadRequest(response.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"DeleteExercise --> Error al eliminar el ejercicio: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetExercisesByDayName")]
        public async Task<ActionResult<GetExercisesByDayNameResponse>> GetExercisesByDayName([FromBody] GetExercisesByDayNameRequest getExercisesByDayNameRequest)
        {
            try
            {
                GetExercisesByDayNameResponse response = await _exerciseApplication.GetExercisesByDayName(getExercisesByDayNameRequest);
                if (response.IsSuccess)
                {
                    Log.Instance.Trace($"Ejercicios obtenidos correctamente");
                    return Ok(response);
                }

                Log.Instance.Trace($"Error al obtener los ejercicios: {response?.Message}");
                return BadRequest(response?.Message);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"GetExercisesByDayName --> Error al obtener los ejercicios: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
