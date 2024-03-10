using VotoSeguro.Domain;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Persistence;

namespace VotoSeguro.DataAccess
{
    public class UserRepository(VotoSeguroContext context) : 
                 BaseRepository<User, VotoSeguroContext>(context), IUserRepository
    {
    }
}
