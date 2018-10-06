namespace Cart.Domain.Entity
{
    using System.Linq;

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

        public void AddItem(CartItem cartItem)
        {
            if (this.Items.Any())
            {
                var firstOrDefault = this.Items.FirstOrDefault(p => p.ProductId == cartItem.ProductId);

                if (firstOrDefault != null)
                {
                    firstOrDefault.Quantity = firstOrDefault.Quantity + cartItem.Quantity;
                }
                else
                {
                    this.Items.Add(cartItem);
                }
            }
            else
            {
                this.Items.Add(cartItem);
            }
        }
    }
}
