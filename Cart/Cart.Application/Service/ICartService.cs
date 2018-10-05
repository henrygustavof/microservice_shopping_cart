namespace Cart.Application.Service
{
    using Dto;
    using System.Threading.Tasks;

    public interface ICartService
    {
        Task<CartDto> GetCartAsync(string buyerId);
        Task<CartDto> UpdateCartAsync(CartDto basket);
        Task<bool> DeleteCartAsync(string buyerId);
    }
}
