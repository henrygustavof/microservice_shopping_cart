namespace Identity.Api.Controllers
{
    using Application.Dto.Input;
    using Application.Dto.Output;
    using Application.Service.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly  IAuthApplicationService _authApplicationService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthApplicationService authApplicationService,
            ILogger<AuthController> logger)
        {
            _authApplicationService = authApplicationService;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(JwTokenDto), 200)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            _logger.LogInformation($"Auth Login!:{model.Email}");
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