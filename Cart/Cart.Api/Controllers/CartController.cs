namespace Cart.Api.Controllers
{
    using Application.Dto;
    using Application.Service;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Threading.Tasks;
    using System.Security.Claims;

    [Produces("application/json")]
    [Route("api/carts")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;

        }

        [ProducesResponseType(typeof(CartDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var basket = await _cartService.GetCartAsync(buyerId);
                        
            return Ok(basket);
        }

        [HttpPost]

        [ProducesResponseType(typeof(CartDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CartDto value)
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var basket = await _cartService.UpdateCartAsync(value);

            return Ok(basket);
        }

        [HttpDelete]
        public void Delete()
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _cartService.DeleteCartAsync(buyerId);

        }
    }
}