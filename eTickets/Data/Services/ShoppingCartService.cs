using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly AppDbContext _context;

        public ShoppingCartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddToCart(int movieId, string cartId)
        {
            var shoppingCartItem = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(s => s.MovieId == movieId && s.ShoppingCartId == cartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    MovieId = movieId,
                    ShoppingCartId = cartId,
                    Quantity = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Quantity++;
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCart(int movieId, string cartId)
        {
            var shoppingCartItem = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(s => s.MovieId == movieId && s.ShoppingCartId == cartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Quantity > 1)
                {
                    shoppingCartItem.Quantity--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ShoppingCartItem>> GetCartItems(string cartId)
        {
            return await _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == cartId)
                .Include(s => s.Movie)
                .ToListAsync();
        }

        public async Task ClearCart(string cartId)
        {
            var cartItems = await _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == cartId)
                .ToListAsync();

            _context.ShoppingCartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
        }
    }
}
