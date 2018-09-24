using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Product.Application.Dto;
using Product.Api.Product.Application.Service;

namespace Product.Api.Product.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductApplicationService _productApplicationService;

        public ProductController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return StatusCode(StatusCodes.Status201Created, _productApplicationService.GetAll());
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductCreateDto productCreateDto)
        {
            _productApplicationService.Create(productCreateDto);
            return StatusCode(StatusCodes.Status201Created, "Product Created!");
        }
    }
}