namespace Identity.Infrastructure.Repositry
{
    using Domain.Entity;
    using Domain.Repository;

    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IdentityContext context): base(context)
        {

        }
    }
}
