namespace Cart.Application.Dto
{
    using System.Collections.Generic;

    public class CartDto
    {
        public string BuyerId { get; set; }
        public List<CartItemDto> Items { get; set; }

        public CartDto()
        {

        }
        public CartDto(string cartId)
        {
            BuyerId = cartId;
            Items = new List<CartItemDto>();
        }
    }
}
