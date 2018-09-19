namespace Cart.Domain.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entity;

    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string cartId);
        IEnumerable<string> GetUsers();
        Task<Cart> UpdateCartAsync(Cart basket);
        Task<bool> DeleteCartAsync(string id);
    }
}
