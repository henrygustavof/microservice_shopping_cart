namespace Identity.Infraestructure.Repositry
{
    using Identity.Domain.Entity;
    using Identity.Domain.Repository;

    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IdentityContext context): base(context)
        {

        }
    }
}
