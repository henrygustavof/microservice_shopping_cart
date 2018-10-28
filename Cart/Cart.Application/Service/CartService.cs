namespace Cart.Application.Service
{
    using AutoMapper;
    using Domain.Entity;
    using Domain.Repository;
    using Dto;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        public async Task<CartOutputDto> GetCartAsync(string buyerId)
        {
            var cart = await _cartRepository.GetCartAsync(buyerId);
            return Mapper.Map<CartOutputDto>(cart);
        }

        public async Task<CartOutputDto> UpdateCartAsync(string buyerId, CartItemCreateDto product)
        {
            var cart = await _cartRepository.GetCartAsync(buyerId);

            cart.AddItem(Mapper.Map<CartItem>(product));
            return Mapper.Map<CartOutputDto>(await _cartRepository.UpdateCartAsync(cart));
        }

        public async Task<List<CartItemOutputDto>> DeleteCartProductAsync(int productId, string buyerId)
        {
            var cart = await _cartRepository.GetCartAsync(buyerId);

            cart.RemoveItem(productId);
            await _cartRepository.UpdateCartAsync(cart);

            return Mapper.Map <List<CartItemOutputDto>>(cart.Items);
        }
    }
}
