using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface ICartService
    {
        Task<Cart> CreateCartAsync(Cart cart);
        Task DeleteCartAsync(Cart cart);
        Task <Cart> GetCartByUserIdAsync(Guid Userid);
        Task UpdateCartAsync(Cart cart);

    }
}
