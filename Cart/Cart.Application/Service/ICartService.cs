namespace Cart.Application.Service
{
    using Dto;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface ICartService
    {
        Task<CartOutputDto> GetCartAsync(string buyerId);
        Task<CartOutputDto> UpdateCartAsync(string buyerId, CartItemCreateDto product);
        Task<bool> DeleteCartAsync(string buyerId);
        Task<List<CartItemOutputDto>> DeleteCartProductAsync(int productId, string buyerId);
    }
}
