using Kintech.RestCA.Transversal.Common;

namespace TFC.Application.DTO.SplitDay.GetRoutineSplits
{
    public class GetRoutineSplitsResponse : BaseResponse
    {
        public List<DayInfoDTO> DayInfo { get; set; } = new List<DayInfoDTO>();
    }
}