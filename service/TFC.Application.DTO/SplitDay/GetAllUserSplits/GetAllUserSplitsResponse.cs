using Kintech.RestCA.Transversal.Common;
using TFC.Application.DTO.Entity;

namespace TFC.Application.DTO.SplitDay.GetAllUserSplits
{
    public class GetAllUserSplitsResponse : BaseResponse
    {
        public List<SplitDayDTO> SplitDays { get; set; } = new List<SplitDayDTO>();
    }
}