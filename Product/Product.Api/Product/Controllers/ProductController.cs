using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Product.Application.Assembler;
using Product.Api.Product.Application.Dto;
using Product.Api.Product.Domain.Repository;

namespace Product.Api.Product.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly ProductCreateAssembler _productCreateAssembler;

        public ProductController(IProductRepository productRepository, ProductCreateAssembler productCreateAssembler)
        {
            _productRepository = productRepository;
            _productCreateAssembler = productCreateAssembler;
        }


        [HttpPost]
        public IActionResult Create([FromBody] ProductCreateDto _productCreateDto)
        {
            Domain.Entity.Product _product = _productCreateAssembler.toEntity(_productCreateDto);
            _productRepository.Create(_product);
            return StatusCode(StatusCodes.Status201Created, "Product Created!");
        }
    }
}