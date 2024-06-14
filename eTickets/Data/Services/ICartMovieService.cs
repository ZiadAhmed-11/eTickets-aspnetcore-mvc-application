using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface ICartMovieService
    {
        Task<List<CartMovie>> GetCartMoviesByCartIdAsync(int CartId);
        Task AddCartMovieAsync(CartMovie CartMovie);
        Task UpdateCartMovie(CartMovie CartMovie);
        Task DeleteCartMovie(int CartMovieId);
    }
}
