using TFC.Application.DTO.SplitDay.AnyadirSplitDay;
using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.GetAllUserSplits;
using TFC.Application.DTO.SplitDay.GetRoutineSplits;
using TFC.Application.DTO.SplitDay.UpdateSplitDay;

namespace TFC.Application.Interface.Application
{
    public interface ISplitDayApplication
    {
        Task<AddSplitDayResponse> CreateSplitDay(AddSplitDayRequest anyadirSplitDayRequest);
        Task<DeleteSplitDayResponse> DeleteSplitDay(DeleteSplitDayRequest deleteSplitDayRequest);
        Task<GetAllUserSplitsResponse> GetAllUserSplits(GetAllUserSplitsRequest getAllUserSplitsResponse);
        Task<GetRoutineSplitsResponse> GetRoutineSplits(GetRoutineSplitsRequest getRoutineSplitsRequest);
        Task<UpdateSplitDayResponse> UpdateSplitDay(UpdateSplitDayRequest actualizarSplitDayRequest);
    }
}