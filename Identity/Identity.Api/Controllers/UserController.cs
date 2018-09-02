namespace Identity.Api.Controllers
{
    using Identity.Application.Dto.Input;
    using Identity.Application.Service.Interfaces;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("api/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrator")]
    public class UserController : Controller
    {

        private readonly IUserApplicationService _userApplicationService;

        public UserController(IUserApplicationService userApplicationService)
        {
            _userApplicationService = userApplicationService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Application.Dto.Output.PaginationDto), 200)]
        public IActionResult GetAll(int page = 1, int pageSize = 10, string sortBy = "id", string sortDirection = "asc")
        {
            return Ok(_userApplicationService.GetAll(page, pageSize, sortBy, sortDirection));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Application.Dto.Output.UserDto), 200)]
        public IActionResult Get(int id)
        {
            return Ok(_userApplicationService.Get(id));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Put(int id, [FromBody]UserDto user)
        {
            _userApplicationService.Update(id, user);
            return Ok("user was updated sucessfully");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Delete(int id)
        {
            _userApplicationService.Delete(id);
            return Ok("user was deleted sucessfully");
        }
    }
}