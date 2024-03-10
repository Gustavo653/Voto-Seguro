using VotoSeguro.Domain;

namespace VotoSeguro.Infrastructure.Service
{
    public interface ITokenService
    {
        Task<string> CreateToken(User userDTO);
    }
}