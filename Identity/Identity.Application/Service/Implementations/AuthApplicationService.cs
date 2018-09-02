namespace Identity.Application.Service.Implementations
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
    using Identity.Application.Service.Interfaces;

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

        public async Task<JwTokenDto> PerformAuthentication(LoginDto model)
        {
  
            Notification notification = await ValidateAuthentication(model);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            return  await GenerateToken(model.Email);
        }

        public async Task<JwTokenDto> PerformRegistration(RegisterDto model)
        {
            Notification notification = ValidateRegistration(model);

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var userResult = await _userInMgr.CreateAsync(user, model.Password);

            var roleResult = await _userInMgr.AddToRoleAsync(user, Roles.Member);
            var claimResult = await _userInMgr.AddClaimAsync(user, new Claim(ClaimTypes.Role, Roles.Member));

            if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
            { 
                notification.AddError(string.Join(". ", from item in userResult.Errors select item.Description));

                throw new ArgumentException(notification.ErrorMessage());
            }

            await _signInMgr.SignInAsync(user, false);

            return await GenerateToken(model.Email);
        }

        private Notification ValidateRegistration(RegisterDto model)
        {
            Notification notification = new Notification();

            if (model == null || string.IsNullOrEmpty(model.Email)
                                  || string.IsNullOrEmpty(model.UserName)
                                  || string.IsNullOrEmpty(model.Password)
                                  )
                
            {
                notification.AddError("Invalid JSON data in request body");
                return notification;
            }

            if (model.UserName.Length < int.Parse(_config["Account:UserNameRequiredLength"]))
            {
                notification.AddError($"UserName must have at least {_config["Account:UserNameRequiredLength"]} characters.");
                return notification;
            }

            return notification;

        }

        private async Task<Notification> ValidateAuthentication(LoginDto model)
        {
            Notification notification = new Notification();

            if (model == null)
            {
                notification.AddError("Invalid JSON data in request body");
                return notification;
            }

            User user = await _userInMgr.FindByEmailAsync(model.Email);

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

            var validCredentials = await _userInMgr.CheckPasswordAsync(user, model.Password);

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

            var signInStatus = await _signInMgr.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);

            if (signInStatus.IsNotAllowed)
            {
                notification.AddError("Invalid credentials.Please try again.");
                return notification;
            }

            return notification;
        }

        private async Task<JwTokenDto> GenerateToken(string email)
        {
            User user = await _userInMgr.FindByEmailAsync(email);

            var userClaims = await _userInMgr.GetClaimsAsync(user);
            var userRoles = await _userInMgr.GetRolesAsync(user);
            var role = userRoles.Any() ? userRoles[0] : string.Empty;
            var claims = new[]
            {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim("role", role)
                    }.Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["Account:ExpirationTimeTokenInMin"])),
                signingCredentials: creds

            );

            return new JwTokenDto
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
            };

        }
    }
}
