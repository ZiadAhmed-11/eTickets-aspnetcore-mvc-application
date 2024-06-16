using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        public CartService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            // Add the cart to the context
            await _context.Cats.AddAsync(cart);
            await _context.SaveChangesAsync();

           /* // Add the CartId to each CartMovie and add them to the context
            foreach (var cartMovie in Cartmovies)
            {
                cartMovie.CartId = cart.CartId;
                await _context.CartMovies.AddAsync(cartMovie);
            }*/

            // Save the changes for CartMovies
            await _context.SaveChangesAsync();

            // Return the created cart
            return cart;
        }

        public async Task DeleteCartAsync(Cart cart)
        {
            var oldcart = await _context.Cats.FirstOrDefaultAsync(c => c.CartId == cart.CartId);
             _context.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartByUserIdAsync(Guid UserId)
        {
            return await _context.Cats.FirstOrDefaultAsync(c=>c.UserId==UserId);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            var oldcart = await _context.Cats.FirstOrDefaultAsync(c => c.CartId == cart.CartId);
            _context.Update(cart);
            await _context.SaveChangesAsync();

        }
    }
}
