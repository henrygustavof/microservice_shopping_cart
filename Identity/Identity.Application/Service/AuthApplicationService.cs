namespace Identity.Application.Service
{
    using Identity.Application.Dto.Input;
    using Identity.Application.Dto.Output;
    using Identity.Application.Notification;
    using Identity.Domain.Entity;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;

    public class AuthApplicationService: IAuthApplicationService
    {
        private readonly SignInManager<User> _signInMgr;
        private readonly UserManager<User> _userInMgr;

        public AuthApplicationService(SignInManager<User> signInMgr,
            UserManager<User> userInMgr)
        {
            _signInMgr = signInMgr;
            _userInMgr = userInMgr;
        }

        public async Task<JwTokenDto> PerformAuthentication(LoginDto login)
        {

            Notification notification = new Notification();

            if (notification.HasErrors())
            {
                throw new ArgumentException(notification.ErrorMessage());
            }

            User user = await _userInMgr.FindByEmailAsync(login.Email);

            if (user != null)
            {

                if (user.Disabled)
                {
                    notification.AddError("Your account is disabled, please contact with the web master");
                    
                }

                if (!user.EmailConfirmed)
                {

                    notification.AddError("Please confirm your email or contact with the web master");
                }

                var validCredentials = await _userInMgr.CheckPasswordAsync(user, login.Password);

                // When a user is lockedout, this check is done to ensure that even if the credentials are valid
                // the user can not login until the lockout duration has passed
                if (await _userInMgr.IsLockedOutAsync(user))
                {
                    notification.AddError("Your account has been locked out due to multiple failed login attempts.");
                    
                }
                // if user is subject to lockouts and the credentials are invalid
                // record the failure and check if user is lockedout and display message, otherwise,
                // display the number of attempts remaining before lockout
                else if (await _userInMgr.GetLockoutEnabledAsync(user) && !validCredentials)
                {
                    // Record the failure which also may cause the user to be locked out
                    await _userInMgr.AccessFailedAsync(user);

                    if (await _userInMgr.IsLockedOutAsync(user))
                    {
                        notification.AddError("Your account has been locked out due to multiple failed login attempts.");
                    }
                    else
                    {
                        int accessFailedCount = await _userInMgr.GetAccessFailedCountAsync(user);

                        notification.AddError("Invalid credentials. You have few more attempt(s) before your account gets locked out..");
                    }
                    
                }
                else if (!validCredentials)
                {
                    notification.AddError("Invalid credentials. Please try again.");                }
                else
                {
                    await _userInMgr.ResetAccessFailedCountAsync(user);

                   var signInStatus = await _signInMgr.PasswordSignInAsync(user.UserName, login.Password, login.RememberMe, false);

                    if (signInStatus.Succeeded)
                    {
                        return new JwTokenDto { access_token ="token" };
                    }

                    if (signInStatus.IsNotAllowed)
                    {
                        notification.AddError("Invalid credentials.Please try again.");

                    }
                }
            }
            else
            {
                notification.AddError("Invalid credentials.Please try again.");
            }

            return new JwTokenDto { access_token = "error_token" };
        }
    }
}
