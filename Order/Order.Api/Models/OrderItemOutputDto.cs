namespace Order.Api.Models
{
    public class OrderItemOutputDto
    {

        public string  ProductName { get; set; }
        public string  PictureUrl { get; set; }
        public decimal Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public string  Currency { get; set; }
    }
}
