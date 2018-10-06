namespace Order.Api.Models
{
    public class OrderItemInputDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; }
    }
}
