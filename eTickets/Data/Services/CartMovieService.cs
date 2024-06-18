using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class CartMovieService : ICartMovieService
    {
        private readonly AppDbContext _context;
        public CartMovieService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCartMovieAsync(CartMovie CartMovie)
        {

            _context.CartMovies.Add(CartMovie);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteCartMovie(int CartMovieId)
        {
            var cartItem = await _context.CartMovies.FindAsync(CartMovieId);
            _context.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CartMovie>> GetCartMoviesByCartIdAsync(int CartId)
        {
            return await _context.CartMovies.Where(cm => cm.CartId == CartId).ToListAsync();

        }

        public bool MovieIsExist(CartMovie cartMovie)
        {
            var cartId= _context.CartMovies.FirstOrDefault(cm=>cm.CartId==cartMovie.CartId&&cm.MovieId==cartMovie.MovieId);
            if(cartId!=null)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateCartMovie(CartMovie CartMovie)
        {
            var cartMovie= await _context.CartMovies.FindAsync(CartMovie.CartId);
            _context.Update(cartMovie);
            await _context.SaveChangesAsync();
        }
    }
}
