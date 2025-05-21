using Microsoft.EntityFrameworkCore;
using TFC.Application.DTO.Entity;
using TFC.Application.DTO.Friend.AddNewUserFriend;
using TFC.Application.DTO.Friend.DeleteFriend;
using TFC.Application.DTO.Friend.GetAllUserFriens;
using TFC.Application.DTO.Friend.GetFriendByFriendCode;
using TFC.Application.Interface.Persistence;
using TFC.Domain.Model.Entity;
using TFC.Infraestructure.Persistence.Context;

namespace TFC.Infraestructure.Persistence.Repository
{
    public class FriendRepository : IFriendRepository
    {
        public readonly ApplicationDbContext _context;

        public FriendRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AddNewUserFriendResponse> AddNewUserFriend(AddNewUserFriendRequest addNewUserFriendRequest)
        {
            AddNewUserFriendResponse response = new AddNewUserFriendResponse();
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == addNewUserFriendRequest.UserEmail);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el usuario con ese email";
                    return response;
                }

                User? friend = await _context.Users.FirstOrDefaultAsync(u => u.FriendCode == addNewUserFriendRequest.FriendCode);
                if (friend == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el amigo con ese codigo";
                    return response;
                }

                if (user.UserId == friend.UserId)
                {
                    response.IsSuccess = false;
                    response.Message = "No puedes agregar a ti mismo como amigo";
                    return response;
                }

                UserFriend userFriend = new UserFriend()
                {
                    UserId = user.UserId,
                    FriendId = friend.UserId
                };

                await _context.UserFriends.AddAsync(userFriend);
                await _context.SaveChangesAsync();

                response.IsSuccess = true;
                response.Message = "Amigo agregado correctamente";
                response.FriendId = friend.UserId;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<DeleteFriendResponse> DeleteFriend(DeleteFriendRequest deleteFriendRequest)
        {
            DeleteFriendResponse response = new DeleteFriendResponse();
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == deleteFriendRequest.UserEmail);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el usuario con ese email";
                    return response;
                }

                User? friend = await _context.Users.FirstOrDefaultAsync(u => u.FriendCode == deleteFriendRequest.FriendCode);
                if (friend == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró el amigo con ese codigo";
                    return response;
                }

                UserFriend? userFriend = await _context.UserFriends.FirstOrDefaultAsync(uf => uf.UserId == user.UserId && uf.FriendId == friend.UserId);
                if (userFriend == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró la relación de amistad";
                    return response;
                }

                _context.UserFriends.Remove(userFriend);
                await _context.SaveChangesAsync();
            
                response.IsSuccess = true;
                response.Message = "Amigo eliminado correctamente";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<GetAllUserFriendsResponse> GetAllUserFriends(GetAllUserFriendsRequest getAllUserFriendsRequest)
        {
            GetAllUserFriendsResponse response = new GetAllUserFriendsResponse();

            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == getAllUserFriendsRequest.UserEmail);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Usuario no encontrado";
                    return response;
                }

                List<UserFriend> userFriends = await _context.UserFriends
                    .Where(uf => uf.UserId == user.UserId)
                    .ToListAsync();

                if (!userFriends.Any())
                {
                    response.IsSuccess = true;
                    response.Message = "El usuario no tiene amigos registrados";
                    response.Friends = new List<UserDTO>();
                    return response;
                }

                List<long> friendIds = userFriends.Select(uf => uf.FriendId).ToList();
                List<UserDTO> friends = await _context.Users
                    .Where(u => friendIds.Contains(u.UserId))
                    .Select(u => new UserDTO
                    {
                        UserId = u.UserId,
                        Dni = u.Dni,
                        Username = u.Username,
                        Surname = u.Surname,
                        FriendCode = u.FriendCode,
                        Email = u.Email,
                        Password = "*************"
                    }).ToListAsync();

                response.Friends = friends;
                response.IsSuccess = true;
                response.Message = "Lista de amigos obtenida correctamente";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error al obtener amigos: {ex.Message}";
                response.Friends = new List<UserDTO>();
            }

            return response;
        }

        public async Task<GetFriendByFriendCodeResponse> GetFriendByFriendCode(GetFriendByFriendCodeRequest getFriendByFriendCodeRequest)
        {
            GetFriendByFriendCodeResponse response = new GetFriendByFriendCodeResponse();
            try
            {
                User? user = _context.Users.FirstOrDefault(u => u.FriendCode == getFriendByFriendCodeRequest.FriendCode);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "No se encontró a la persona con ese friend code";
                    return response;
                }

                UserDTO userDTO = new UserDTO()
                {
                    Dni = user.Dni,
                    Username = user.Username,
                    Surname = user.Surname,
                    FriendCode = user.FriendCode,
                    Password = "********",
                    Email = user.Email
                };

                response.IsSuccess = true;
                response.Message = "Consulta correcta";
                response.UserDTO = userDTO; 
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
