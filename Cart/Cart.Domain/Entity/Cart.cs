namespace Cart.Domain.Entity
{
    using System.Collections.Generic;

    public class Cart
    {
        public string BuyerId { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart()
        {

        }
        public Cart(string buyerId)
        {
            BuyerId = buyerId;
            Items = new List<CartItem>();
        }
    }
}
