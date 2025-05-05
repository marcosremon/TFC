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
            if (anyadirSplitDayRequest == null
                || anyadirSplitDayRequest.DayName == null
                || anyadirSplitDayRequest.UserId == null
                || anyadirSplitDayRequest.RoutineId == null)
            {
                return new AddSplitDayResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: AddSplitDayRequest is null or required fields are missing."
                };
            }

            return await _splitDayRepository.CreateSplitDay(anyadirSplitDayRequest);
        }

        public async Task<DeleteSplitDayResponse> DeleteSplitDay(DeleteSplitDayRequest deleteSplitDayRequest)
        {
            if (deleteSplitDayRequest == null
               || deleteSplitDayRequest.DayName == null
               || deleteSplitDayRequest.UserId == null
               || deleteSplitDayRequest.RoutineId == null)
            {
                return new DeleteSplitDayResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: DeleteSplitDayRequest is null or required fields are missing."
                };
            }

            return await _splitDayRepository.DeleteSplitDay(deleteSplitDayRequest);
        }

        public async Task<GetAllUserSplitsResponse> GetAllUserSplits(GetAllUserSplitsRequest getAllUserSplitsRequest)
        {
            if (getAllUserSplitsRequest == null || getAllUserSplitsRequest.UserId == null)
            {
                return new GetAllUserSplitsResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: GetAllUserSplitsRequest is null or UserId is missing."
                };
            }

            return await _splitDayRepository.GetAllUserSplits(getAllUserSplitsRequest);
        }

        public async Task<ActualizarSplitDayResponse> UpdateSplitDay(ActualizarSplitDayRequest actualizarSplitDayRequest)
        {
            if (actualizarSplitDayRequest == null
               || actualizarSplitDayRequest.DayName == null
               || actualizarSplitDayRequest.UserId == null
               || actualizarSplitDayRequest.RoutineId == null)
            {
                return new ActualizarSplitDayResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: ActualizarSplitDayRequest is null or required fields are missing."
                };
            }

            return await _splitDayRepository.UpdateSplitDay(actualizarSplitDayRequest);
        }
    }
}