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
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;

    [Produces("application/json")]
    [Route("api/carts")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;

        }

        [HttpGet]
        [ProducesResponseType(typeof(CartOutputDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _logger.LogInformation($"Cart Get! buyerId:{buyerId}");

            var basket = await _cartService.GetCartAsync(buyerId);
                        
            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CartOutputDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CartItemCreateDto product)
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _logger.LogInformation($"Cart Post! buyerId:{buyerId}");

            var basket = await _cartService.UpdateCartAsync(buyerId, product);

            return Ok(basket);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete()
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _logger.LogInformation($"Cart Delete! buyerId:{buyerId}");

            await _cartService.DeleteCartAsync(buyerId);

            return Ok("cart was deleted");
        }

        [HttpDelete("product/{productId}")]
        [ProducesResponseType(typeof(List<CartItemOutputDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            _logger.LogInformation($"Cart DeleteProduct! buyerId:{buyerId}, ProductId: {productId}");

            return Ok( await _cartService.DeleteCartProductAsync(productId, buyerId));

        }
    }
}