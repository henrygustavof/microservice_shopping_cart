namespace Product.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Product.Application.Dto;
    using Product.Application.Service;
    using System.Collections.Generic;
    using System.Net;

    [Produces("application/json")]
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryApplicationService _categoryApplicationService;

        public CategoryController(ICategoryApplicationService categoryApplicationService)
        {
            _categoryApplicationService = categoryApplicationService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryOutputDto), (int)HttpStatusCode.OK)]
        public IActionResult Get(int id)
        {
            return Ok(_categoryApplicationService.Get(id));
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryOutputDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            return Ok(_categoryApplicationService.GetAll());
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult Create([FromBody] CategoryCreateDto model)
        {
            _categoryApplicationService.Create(model);
            return Ok("Category Created!");
        }
    }
}