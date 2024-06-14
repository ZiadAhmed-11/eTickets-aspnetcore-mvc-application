using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface ICartService
    {
        Task<Cart> CreateCartAsync(Cart cart,List<CartMovie> Cartmovies);
        Task DeleteCartAsync(Cart cart);
        Task <Cart> GetCartAsync(int id);
        Task<Cart> UpdateCartAsync(Cart cart);

    }
}
