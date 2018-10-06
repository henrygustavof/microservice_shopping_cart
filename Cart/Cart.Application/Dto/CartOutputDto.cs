namespace Cart.Application.Dto
{
    using System.Collections.Generic;

    public class CartOutputDto
    {
        public decimal Total { get; set; }
        public List<CartItemOutputDto> Items { get; set; }

        public CartOutputDto()
        {
            Items = new List<CartItemOutputDto>();
        }
    }
}
