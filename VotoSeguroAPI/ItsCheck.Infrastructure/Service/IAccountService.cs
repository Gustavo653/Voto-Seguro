using ItsCheck.DTO;
using ItsCheck.DTO.Base;

namespace ItsCheck.Infrastructure.Service
{
    public interface IAccountService
    {
        Task<ResponseDTO> CreateUser(UserDTO userDTO);
        Task<ResponseDTO> UpdateUser(int id, UserDTO userDTO);
        Task<ResponseDTO> RemoveUser(int id);
        Task<ResponseDTO> GetUsers();
        Task<ResponseDTO> GetCurrent();
        Task<ResponseDTO> Login(UserLoginDTO userLoginDTO);
    }
}