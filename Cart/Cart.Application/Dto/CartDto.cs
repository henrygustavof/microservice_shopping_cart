namespace Cart.Application.Dto
{
    using System.Collections.Generic;

    public class CartDto
    {
        public decimal Total { get; set; }
        public List<CartItemDto> Items { get; set; }

        public CartDto()
        {
            Items = new List<CartItemDto>();
        }
    }
}
