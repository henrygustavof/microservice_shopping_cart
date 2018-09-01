namespace Identity.Application.Service
{
    using Dto.Input;
    using Dto.Output;
    using Notification;
    using Domain.Entity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using System.IdentityModel.Tokens.Jwt;
    using Microsoft.IdentityModel.Tokens;

    public class AuthApplicationService: IAuthApplicationService
    {
        private readonly SignInManager<User> _signInMgr;
        private readonly UserManager<User> _userInMgr;
        private readonly IConfiguration _config;

        public AuthApplicationService(SignInManager<User> signInMgr,
            UserManager<User> userInMgr, IConfiguration config)
        {
            _signInMgr = signInMgr;
            _userInMgr = userInMgr;
            _config = config;
        }

        public async Task<JwTokenDto> PerformAuthentication(LoginDto login)
        {

            Notification notification = await Validation(login);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            return  await GenerateToken(login);
        }

        private async Task<Notification> Validation(LoginDto login)
        {
            Notification notification = new Notification();

            User user = await _userInMgr.FindByEmailAsync(login.Email);

            if (user == null)
            {
                notification.AddError("Invalid credentials.Please try again.");
                return notification;
            }

            if (user.Disabled)
            {
                notification.AddError("Your account is disabled, please contact with the web master");
                return notification;

            }

            if (!user.EmailConfirmed)
            {
                notification.AddError("Please confirm your email or contact with the web master");
                return notification;
            }

            var validCredentials = await _userInMgr.CheckPasswordAsync(user, login.Password);

            if (await _userInMgr.IsLockedOutAsync(user))
            {
                notification.AddError($"Your account has been locked out for {_config["Account:DefaultAccountLockoutTimeSpan"]} minutes due to multiple failed login attempts.");
                return notification;
            }

            if (await _userInMgr.GetLockoutEnabledAsync(user) && !validCredentials)
            {
                await _userInMgr.AccessFailedAsync(user);

                if (await _userInMgr.IsLockedOutAsync(user))
                {
                    notification.AddError(string.Format(
                                    "Your account has been locked out for {0} minutes due to multiple failed login attempts.",
                                    _config["Account:DefaultAccountLockoutTimeSpan"]));

                    return notification;
                }

                int accessFailedCount = await _userInMgr.GetAccessFailedCountAsync(user);

                int attemptsLeft = Convert.ToInt32(_config["Account:MaxFailedAccessAttemptsBeforeLockout"]) - accessFailedCount;


                notification.AddError(string.Format(
                                    "Invalid credentials. You have {0} more attempt(s) before your account gets locked out..",
                                    attemptsLeft));

                return notification;

            }

            if (!validCredentials)
            {
                notification.AddError("Invalid credentials. Please try again.");
                return notification;

            }

            await _userInMgr.ResetAccessFailedCountAsync(user);

            var signInStatus = await _signInMgr.PasswordSignInAsync(user.UserName, login.Password, login.RememberMe, false);

            if (signInStatus.IsNotAllowed)
            {
                notification.AddError("Invalid credentials.Please try again.");
                return notification;
            }

            return notification;
        }

        private async Task<JwTokenDto> GenerateToken(LoginDto login)
        {
            var user = await _userInMgr.FindByEmailAsync(login.Email);

            var userClaims = await _userInMgr.GetClaimsAsync(user);
            var userRoles = await _userInMgr.GetRolesAsync(user);
            var claims = new[]
            {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("role", userRoles.Any()? userRoles[0] : string.Empty)
                    }.Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Tokens:ExpirationTimeTokenInMin"])),
                signingCredentials: creds

            );

            return new JwTokenDto
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
            };

        }
    }
}
