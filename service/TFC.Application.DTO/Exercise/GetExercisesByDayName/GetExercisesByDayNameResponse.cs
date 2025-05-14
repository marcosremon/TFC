using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.DTO.Exercise.GetExercisesByDayName
{
    public class GetExercisesByDayNameResponse : BaseResponse
    {
        public List<ExerciseDTO> Exercises { get; set; } = new List<ExerciseDTO>();
    }
}