using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.UpdateSplitDay;
using TFC.Application.Interface.Application;
using TFC.Application.Interface.Persistence;
using TFC.Transversal.Logs;

namespace TFC.Application.Main
{
    public class SplitDayApplication : ISplitDayApplication
    {
        private readonly ISplitDayRepository _splitDayRepository;

        public SplitDayApplication(ISplitDayRepository splitDayRepository)
        {
            _splitDayRepository = splitDayRepository;
        }

        public async Task<DeleteSplitDayResponse> DeleteSplitDay(DeleteSplitDayRequest deleteSplitDayRequest)
        {
            if (deleteSplitDayRequest == null
               || deleteSplitDayRequest.DayName == null
               || deleteSplitDayRequest.UserId == null
               || deleteSplitDayRequest.RoutineId == null)
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new DeleteSplitDayResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: DeleteSplitDayRequest is null or required fields are missing."
                };
            }

            return await _splitDayRepository.DeleteSplitDay(deleteSplitDayRequest);
        }

        public async Task<UpdateSplitDayResponse> UpdateSplitDay(UpdateSplitDayRequest actualizarSplitDayRequest)
        {
            if (actualizarSplitDayRequest == null
               || actualizarSplitDayRequest.DayName == null
               || actualizarSplitDayRequest.UserId == null
               || actualizarSplitDayRequest.RoutineId == null)
            {
                Log.Instance.Trace($"Invalid request: el request esta vacio o tiene algun campo nulo o vacio");
                return new UpdateSplitDayResponse
                {
                    IsSuccess = false,
                    Message = "Invalid request: ActualizarSplitDayRequest is null or required fields are missing."
                };
            }

            return await _splitDayRepository.UpdateSplitDay(actualizarSplitDayRequest);
        }
    }
}