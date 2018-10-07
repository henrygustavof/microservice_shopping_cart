namespace Order.Api.Models
{
    public class OrderItemOutputDto
    {
        public int ProductId { get; set; }
        public string  ProductName { get; set; }
        public string  PictureUrl { get; set; }
        public decimal Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public string  Currency { get; set; }
    }
}
