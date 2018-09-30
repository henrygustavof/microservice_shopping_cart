namespace Product.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Product.Application.Dto;
    using Product.Application.Service;
    using System.Collections.Generic;
    using System.Net;

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
        public IActionResult Create([FromBody] ProductCreateDto model)
        {
            _productApplicationService.Create(model);
            return Ok("Product Created!");
        }
    }
}