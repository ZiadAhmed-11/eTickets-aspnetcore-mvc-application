using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IShoppingCartService
    {
        Task AddToCart(int movieId, string cartId);
        Task RemoveFromCart(int movieId, string cartId);
        Task<List<ShoppingCartItem>> GetCartItems(string cartId);
        Task ClearCart(string cartId);
    }
}
