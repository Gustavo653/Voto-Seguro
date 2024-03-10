using VotoSeguro.DTO;
using VotoSeguro.DTO.Base;

namespace VotoSeguro.Infrastructure.Service
{
    public interface IAccountService
    {
        Task<ResponseDTO> CreateUser(UserDTO userDTO);
        Task<ResponseDTO> UpdateUser(Guid id, UserDTO userDTO);
        Task<ResponseDTO> RemoveUser(Guid id);
        Task<ResponseDTO> GetUsers();
        Task<ResponseDTO> GetCurrent();
        Task<ResponseDTO> Login(UserLoginDTO userLoginDTO);
    }
}