namespace Product.Api.Controllers
{
    using Application.Dto;
    using Application.Service;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Product.Application.Contracts;
    using System.Collections.Generic;
    using System.Net;

    [Produces("application/json")]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductApplicationService _productApplicationService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductApplicationService productApplicationService,
            ILogger<ProductController> logger)
        {
            _productApplicationService = productApplicationService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductOutputDto), (int)HttpStatusCode.OK)]
        public IActionResult Get(int id)
        {
            _logger.LogInformation($"Product Get!:{id}");
            return Ok(_productApplicationService.Get(id));
        }
        [HttpGet]
        [ProducesResponseType(typeof(PaginationOutputDto), 200)]
        public IActionResult GetAll(int page = 1, int pageSize = 10, string sortBy = "product_id", string sortDirection = "asc")
        {
            _logger.LogInformation("Product GetAll!");
            return Ok(_productApplicationService.GetAll(page, pageSize, sortBy, sortDirection));
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult Create([FromBody] ProductCreateDto model)
        {
            _productApplicationService.Create(model);
            _logger.LogInformation("Product Create!");
            return Ok("Product Created!");
        }
    }
}