namespace Identity.Api.Controllers
{
    using Application.Dto.Input;
    using Application.Dto.Output;
    using Application.Service;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
 
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly  IAuthApplicationService _authApplicationService;

        public AuthController(IAuthApplicationService authApplicationService)
        {
            _authApplicationService = authApplicationService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(JwTokenDto), 200)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            return Ok( await _authApplicationService.PerformAuthentication(model));
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(JwTokenDto), 200)]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            return Ok(await _authApplicationService.PerformRegistration(model));
        }
    }
}