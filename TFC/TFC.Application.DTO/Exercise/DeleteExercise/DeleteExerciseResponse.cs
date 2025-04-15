using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.Exercise.AddExercise
{
    public class DeleteExerciseResponse : BaseResponse
    {
        public UserDTO? UserDTO { get; set; } = new UserDTO();
    }
}