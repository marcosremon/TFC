using TFC.Application.Interface;
using TFC.Infraestructure.Interface;

namespace TFC.Domain.Core
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserRepository _userRepository;

        public UserDomain(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}