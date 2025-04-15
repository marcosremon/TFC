using TFC.Application.DTO.SplitDay.ActualizarSplitDay;
using TFC.Application.DTO.SplitDay.AnyadirSplitDay;
using TFC.Application.DTO.SplitDay.DeleteSplitDay;
using TFC.Application.DTO.SplitDay.GetAllUserSplits;
using TFC.Application.Interface.Persistence;
using TFC.Infraestructure.Persistence.Context;

namespace TFC.Infraestructure.Persistence.Repository
{
    public class SplitDayRepository : ISplitDayRepository
    {
        private readonly ApplicationDbContext _context;

        public SplitDayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddSplitDayResponse> CreateSplitDay(AddSplitDayRequest anyadirSplitDayRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<DeleteSplitDayResponse> DeleteSplitDay(DeleteSplitDayRequest deleteSplitDayRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAllUserSplitsResponse> GetAllUserSplits(GetAllUserSplitsRequest getAllUserSplitsResponse)
        {
            throw new NotImplementedException();
        }

        public async Task<ActualizarSplitDayResponse> UpdateSplitDay(ActualizarSplitDayRequest actualizarSplitDayRequest)
        {
            throw new NotImplementedException();
        }
    }
}