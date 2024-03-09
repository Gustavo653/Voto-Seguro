using ItsCheck.Domain.Identity;

namespace ItsCheck.Infrastructure.Service
{
    public interface ITokenService
    {
        Task<string> CreateToken(User userDTO);
    }
}