using TFC.DDBB.DatabaseConnection;
using TFC.Infraestructure.Interface;

namespace TFC.Infraestructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}