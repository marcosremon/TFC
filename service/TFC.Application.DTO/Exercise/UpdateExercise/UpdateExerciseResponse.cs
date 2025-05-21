using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.Exercise.UpdateExercise
{
    public class UpdateExerciseResponse : BaseResponse
    {
        public UserDTO? UserDTO { get; set; } = new UserDTO();
    }
}