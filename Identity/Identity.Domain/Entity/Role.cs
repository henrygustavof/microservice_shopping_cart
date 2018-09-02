namespace Identity.Domain.Entity
{
    using Microsoft.AspNetCore.Identity;
    public class Role : IdentityRole<int>
    {
    }

    public static class Roles
    {
        public const string Admin = "admin";
        public const string Member = "member";
    }
}
