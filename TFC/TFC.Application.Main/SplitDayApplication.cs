using TFC.Application.DTO.SplitDay.ActualizarSplitDay;
using TFC.Application.DTO.SplitDay.AnyadirSplitDay;
using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.GetAllUserSplits;
using TFC.Application.Interface.Application;
using TFC.Application.Interface.Persistence;

namespace TFC.Application.Main
{
    public class SplitDayApplication : ISplitDayApplication
    {
        private readonly ISplitDayRepository _splitDayRepository;

        public SplitDayApplication(ISplitDayRepository splitDayRepository)
        {
            _splitDayRepository = splitDayRepository;
        }

        public async Task<AddSplitDayResponse> CreateSplitDay(AddSplitDayRequest anyadirSplitDayRequest)
        {
            return await _splitDayRepository.CreateSplitDay(anyadirSplitDayRequest);
        }

        public async Task<DeleteSplitDayResponse> DeleteSplitDay(DeleteSplitDayRequest deleteSplitDayRequest)
        {
            return await _splitDayRepository.DeleteSplitDay(deleteSplitDayRequest);
        }

        public async Task<GetAllUserSplitsResponse> GetAllUserSplits(GetAllUserSplitsRequest getAllUserSplitsResponse)
        {
            return await _splitDayRepository.GetAllUserSplits(getAllUserSplitsResponse);
        }

        public async Task<ActualizarSplitDayResponse> UpdateSplitDay(ActualizarSplitDayRequest actualizarSplitDayRequest)
        {
            return await _splitDayRepository.UpdateSplitDay(actualizarSplitDayRequest);
        }
    }
}