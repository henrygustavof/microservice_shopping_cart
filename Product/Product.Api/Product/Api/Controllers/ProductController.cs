using Microsoft.AspNetCore.Mvc;
using Product.Api.Product.Application.Dto;
using Product.Api.Product.Application.Service;
using System.Collections.Generic;
using System.Net;

namespace Product.Api.Product.Api.Controllers
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductOutputDto), (int)HttpStatusCode.OK)]
        public IActionResult Get(int id)
        {
            return Ok(_productApplicationService.Get(id));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProductOutputDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return  Ok(_productApplicationService.GetAll());
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult Create([FromBody] ProductCreateDto productCreateDto)
        {
            _productApplicationService.Create(productCreateDto);
            return Ok("Product Created!");
        }
    }
}