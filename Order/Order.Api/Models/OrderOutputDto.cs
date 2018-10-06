namespace Order.Api.Models
{
    using System.Collections.Generic;

    public class OrderOutputDto
    {
        public int Id { get; set; }
        public string  FullName { get; set; }
        public string Address { get; set; }
        public string OrderDate { get; set; }
        public decimal  Total { get; set; }
        public string Currency { get; set; }

        public List <OrderItemOutputDto>  OrderItems { get; set; }
    }
}
