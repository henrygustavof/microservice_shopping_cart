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
                    firstOrDefault.Unit = firstOrDefault.Unit + cartItem.Unit;
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

        public void RemoveItem(int productId)
        {
            if (this.Items.Any())
            {
                  this.Items.Remove(this.Items.FirstOrDefault(p => p.ProductId == productId));
            }
        }
    }
}
