namespace Cart.Domain.Entity
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string Currency { get; set; }
        public int Unit { get; set; }
        public string PictureUrl { get; set; }

    }
}
