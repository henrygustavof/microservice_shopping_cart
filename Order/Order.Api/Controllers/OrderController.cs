namespace Order.Api.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;

    [Produces("application/json")]
    [Route("api/orders")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<OrderHeaderOutput>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            List<OrderHeaderOutput> orders = new List<OrderHeaderOutput>
            {
                new OrderHeaderOutput
                {
                    Id = 1,
                    Address = "Lima 123",
                    FullName = " Juan Perez",
                    OrderDate = "10/10/2018",
                    Total =300,
                    Currency = "USD"
                },
                new OrderHeaderOutput
                {
                    Id = 2,
                    Address = "Lima 123",
                    FullName = " Juan Perez",
                    OrderDate = "11/11/2018",
                    Total = 200,
                    Currency = "USD"
                }
            };

            return Ok( await Task.Run( () => orders));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderOutputDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(int id)
        {
            OrderOutputDto order = new OrderOutputDto
            {
                Id = 1,
                Address = "Lima 123",
                FullName = " Juan Perez",
                OrderDate = "10/10/2018",
                Total = 300,
                Currency = "USD",
                OrderItems = new List<OrderItemOutputDto>
                {
                    new OrderItemOutputDto
                    {
                        Currency = "USD",
                        PictureUrl = "https://hfuentesstorage.blob.core.windows.net/images/adidas_black_tshirt.jpg",
                        ProductName = "adidas black t-shirt",
                        Total = 100,
                        Unit = 1,
                        UnitPrice = 100
                    },
                    new OrderItemOutputDto
                    {
                        Currency = "USD",
                        PictureUrl = "https://hfuentesstorage.blob.core.windows.net/images/adidas_white_tshirt.jpg",
                        ProductName = "adidas white t-shirt",
                        Total = 200,
                        Unit = 2,
                        UnitPrice = 100
                    }
                }
            };

            return Ok(await Task.Run(() => order));
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult Create([FromBody] OrderInputDto model)
        {
            return Ok("Order Created!");
        }
    }
}