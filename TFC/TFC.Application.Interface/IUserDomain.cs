using TFC.Application.DTO.EntityDTO;

namespace TFC.Application.Interface
{
    public interface IUserDomain
    {
        public Task<UserDTO> GetUserById(long id);
        public Task<List<UserDTO>> GetUsers();
        public Task<UserDTO> CreateUser(UserDTO userDTO);
        public Task<UserDTO> UpdateUser(UserDTO userDTO);
        public Task<bool> DeleteUser(long userId);
    }
}