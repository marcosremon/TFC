using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.Exercise.AddExercise
{
    public class AddExerciseResponse : BaseResponse
    {
        public UserDTO? UserDTO { get; set; } = new UserDTO();
    }
}