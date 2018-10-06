namespace Cart.Application.Service
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Dto;
    using Domain.Repository;
    using Domain.Entity;
    using System.Collections.Generic;

    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> DeleteCartAsync(string buyerId)
        {
            return await _cartRepository.DeleteCartAsync(buyerId);
        }

        public async Task<CartDto> GetCartAsync(string buyerId)
        {
            var cart = await _cartRepository.GetCartAsync(buyerId) ??
                       new Cart { BuyerId = buyerId, Items = new List<CartItem>() };

            return Mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> UpdateCartAsync(string buyerId, CartDto basket)
        {
            var myBasket = Mapper.Map<Cart>(basket);
            myBasket.BuyerId = buyerId;
            return Mapper.Map<CartDto>(await _cartRepository.UpdateCartAsync(myBasket));
        }
    }
}
