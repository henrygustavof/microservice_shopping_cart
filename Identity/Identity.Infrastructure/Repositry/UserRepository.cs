namespace Identity.Infraestructure.Repositry
{
    using Identity.Domain.Entity;
    using Identity.Domain.Repository;

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IdentityContext context): base(context)
        {

        }
    }
}
