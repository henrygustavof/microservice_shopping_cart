namespace Cart.Api.Controllers
{
    using Cart.Application.Dto;
    using Cart.Application.Service;
    using Microsoft.AspNetCore.Mvc;
    using System.Net;
    using System.Threading.Tasks;

    [Produces("application/json")]
    [Route("api/carts")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;

        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CartDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _cartService.GetCartAsync(id);

            return Ok(basket);
        }

        // POST api/values
        [HttpPost]

        [ProducesResponseType(typeof(CartDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CartDto value)
        {
            var basket = await _cartService.UpdateCartAsync(value);

            return Ok(basket);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _cartService.DeleteCartAsync(id);

        }
    }
}