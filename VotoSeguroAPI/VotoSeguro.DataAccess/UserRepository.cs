using VotoSeguro.Domain.Identity;
using VotoSeguro.Infrastructure.Repository;
using VotoSeguro.Persistence;

namespace VotoSeguro.DataAccess
{
    public class UserRepository : BaseRepository<User, VotoSeguroContext>, IUserRepository
    {
        public UserRepository(VotoSeguroContext context) : base(context)
        {
        }
    }
}
