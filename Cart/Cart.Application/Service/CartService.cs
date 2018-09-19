namespace Cart.Application.Service
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Cart.Application.Dto;
    using Cart.Domain.Repository;
    using Cart.Domain.Entity;

    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> DeleteCartAsync(string id)
        {
            return await _cartRepository.DeleteCartAsync(id);
        }

        public async Task<CartDto> GetCartAsync(string cartId)
        {
            return Mapper.Map<CartDto>(await _cartRepository.GetCartAsync(cartId));
        }

        public async Task<CartDto> UpdateCartAsync(CartDto basket)
        {
            return Mapper.Map<CartDto>(await _cartRepository.UpdateCartAsync(Mapper.Map<Cart>(basket)));
        }
    }
}
