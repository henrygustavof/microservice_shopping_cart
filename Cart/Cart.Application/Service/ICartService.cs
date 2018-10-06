namespace Cart.Application.Service
{
    using Dto;
    using System.Threading.Tasks;

    public interface ICartService
    {
        Task<CartOutputDto> GetCartAsync(string buyerId);
        Task<CartOutputDto> UpdateCartAsync(string buyerId, CartItemCreateDto product);
        Task<bool> DeleteCartAsync(string buyerId);
    }
}
