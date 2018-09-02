namespace Identity.Infrastructure.Initializer
{
    using Domain.Entity;
    using Repositry;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class DbInitializer
    {
        private readonly RoleManager<Role> _roleMgr;
        private readonly UserManager<User> _userInMgr;

        public DbInitializer(IdentityContext context, UserManager<User> userMgr,
                             RoleManager<Role> roleMgr)
        {
            _userInMgr = userMgr;
            _roleMgr = roleMgr;

            context.Database.EnsureCreated();

        }

        public async Task Seed()
        {
            await SeedAdmin();
            await SeedMember();

        }

        public async Task SeedAdmin()
        {
            var userName = "admin";
            var user = await _userInMgr.FindByNameAsync(userName);

            // Add User
            if (user == null)
            {
                string roleName = Roles.Admin;

                if (!await _roleMgr.RoleExistsAsync(roleName))
                {
                    var role = new Role { Name = roleName };
                    await _roleMgr.CreateAsync(role);
                }

                user = new User
                {
                    UserName = userName,
                    Email = "admin@henrygustavo.com",
                    EmailConfirmed = true,
                    PhoneNumber = "530-685-2496"
                };

                var userResult = await _userInMgr.CreateAsync(user, "P@$$w0rd");
                var roleResult = await _userInMgr.AddToRoleAsync(user, roleName);
                var claimResult = await _userInMgr.AddClaimAsync(user, new Claim(ClaimTypes.Role, roleName));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }

            }
        }

        public async Task SeedMember()
        {
            var userName = "member";
            var user = await _userInMgr.FindByNameAsync(userName);

            // Add User
            if (user == null)
            {
                string roleName = Roles.Member;

                if (!await _roleMgr.RoleExistsAsync(roleName))
                {
                    var role = new Role { Name = roleName };

                    await _roleMgr.CreateAsync(role);
                }

                user = new User
                {
                    UserName = userName,
                    Email = "member@test.com",
                    EmailConfirmed = true,
                    PhoneNumber = "530-685-2496"
                };

                var userResult = await _userInMgr.CreateAsync(user, "P@$$w0rd");
                var roleResult = await _userInMgr.AddToRoleAsync(user, roleName);
                var claimResult = await _userInMgr.AddClaimAsync(user, new Claim(ClaimTypes.Role, roleName));

                if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }

            }
        }
    }
}
