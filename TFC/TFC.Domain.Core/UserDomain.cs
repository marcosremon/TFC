using TFC.Application.DTO.EntityDTO;
using TFC.Application.Interface.Domain;
using TFC.Application.Interface.Persistence;

namespace TFC.Domain.Core
{
    public class UserDomain : IUserDomain
    {
        private readonly IUserRepository _userRepository;

        public UserDomain(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateNewPassword(string email)
        {
            return await _userRepository.CreateNewPassword(email);
        }

        public async Task<UserDTO> CreateUser(UserDTO userDTO)
        {
            return await _userRepository.CreateUser(userDTO);
        }

        public async Task<bool> DeleteUser(long userId)
        {
            return await _userRepository.DeleteUser(userId);
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<UserDTO> UpdateUser(UserDTO userDTO)
        {
            return await _userRepository.UpdateUser(userDTO);
        }
    }
}