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

        [HttpGet]
        [ProducesResponseType(typeof(CartOutputDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var basket = await _cartService.GetCartAsync(buyerId);
                        
            return Ok(basket);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CartOutputDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CartItemCreateDto product)
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var basket = await _cartService.UpdateCartAsync(buyerId, product);

            return Ok(basket);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete()
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _cartService.DeleteCartAsync(buyerId);

            return Ok("cart was deleted");
        }

        [HttpDelete("product/{productId}")]
        [ProducesResponseType(typeof(List<CartItemOutputDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var buyerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok( await _cartService.DeleteCartProductAsync(productId, buyerId));

        }
    }
}