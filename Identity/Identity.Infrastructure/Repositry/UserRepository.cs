namespace Identity.Infrastructure.Repositry
{
    using Domain.Entity;
    using Domain.Repository;

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IdentityContext context): base(context)
        {

        }
    }
}
