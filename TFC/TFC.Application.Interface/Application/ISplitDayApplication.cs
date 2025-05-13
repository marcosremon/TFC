using TFC.Application.DTO.SplitDay.ActualizarSplitDay;
using TFC.Application.DTO.SplitDay.AnyadirSplitDay;
using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.GetAllUserSplits;

namespace TFC.Application.Interface.Application
{
    public interface ISplitDayApplication
    {
        Task<AddSplitDayResponse> CreateSplitDay(AddSplitDayRequest anyadirSplitDayRequest);
        Task<DeleteSplitDayResponse> DeleteSplitDay(DeleteSplitDayRequest deleteSplitDayRequest);
        Task<GetAllUserSplitsResponse> GetAllUserSplits(GetAllUserSplitsRequest getAllUserSplitsResponse);
        Task<ActualizarSplitDayResponse> UpdateSplitDay(UpdateSplitDayRequest actualizarSplitDayRequest);
    }
}