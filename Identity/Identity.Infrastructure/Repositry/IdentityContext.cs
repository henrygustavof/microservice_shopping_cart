namespace Identity.Infraestructure.Repositry
{
    using Identity.Domain.Entity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class IdentityContext:  IdentityDbContext<User, Role, int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
              .ToTable("users");

            modelBuilder.Entity<Role>()
                .ToTable("roles");

            modelBuilder.Entity<IdentityRoleClaim<int>>()
                .ToTable("roles_claims");

            modelBuilder.Entity<IdentityUserRole<int>>()
                .ToTable("user_roles");

            modelBuilder.Entity<IdentityUserClaim<int>>()
                .ToTable("user_claims");

            modelBuilder.Entity<IdentityUserLogin<int>>()
                .ToTable("user_logins");

            modelBuilder.Entity<IdentityUserToken<int>>()
                .ToTable("user_tokens");

        }

    }
}
