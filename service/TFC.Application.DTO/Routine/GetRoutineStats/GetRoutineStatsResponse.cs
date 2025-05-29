using Kintech.RestCA.Transversal.Common;

namespace TFC.Application.DTO.Routine.GetRoutineStats
{
    public class GetRoutineStatsResponse : BaseResponse
    {
        public int routinesCount { get; set; }
        public int exercisesCount { get; set; }
        public int splitsCount { get; set; }
    }
}