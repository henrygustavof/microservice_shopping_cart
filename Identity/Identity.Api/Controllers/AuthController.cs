namespace Identity.Api.Controllers
{
    using Identity.Application.Dto.Input;
    using Identity.Application.Dto.Output;
    using Identity.Application.Service;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
 
    [Produces("application/json")]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        public  IAuthApplicationService _authApplicationService;

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
    }
}