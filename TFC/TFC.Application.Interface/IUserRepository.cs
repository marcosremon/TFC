using TFC.Application.DTO.EntityDTO;

namespace TFC.Infraestructure.Interface
{
    public interface IUserRepository
    {
        public Task<UserDTO> GetUserById(long id);
        public Task<List<UserDTO>> GetUsers();
        public Task<UserDTO> CreateUser(UserDTO userDTO);
        public Task<UserDTO> UpdateUser(UserDTO userDTO);
        public Task<bool> DeleteUser(long userId);
    }
}