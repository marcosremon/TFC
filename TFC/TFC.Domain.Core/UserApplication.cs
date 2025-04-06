
using TFC.Application.Interface;

namespace TFC.Domain.Core
{
    public class UserApplication : IUserDomain
    {
        private readonly IUserRepository _userRepository;

        public UserApplication(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}