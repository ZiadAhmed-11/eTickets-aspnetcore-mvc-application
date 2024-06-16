using eTickets.Data.Base;
using eTickets.Data.Services;
using eTickets.Data;
using eTickets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class MovieService : EntityBaseRepository<Movie>, IMoviesService
{
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cinema>> GetCinemasAsync()
    {
        return await _context.Cinemas.ToListAsync();
    }

    public async Task<IEnumerable<Producer>> GetProducersAsync()
    {
        return await _context.Producers.ToListAsync();
    }

    public async Task<IEnumerable<Movie>> GetAllMoviesWithDetailsAsync()
    {
        return await _context.Movies.Include(m => m.Cinema).ToListAsync();
    }
    public async Task<Movie> GetAsync(int id)
    {
        var movie=await _context.Movies.FirstOrDefaultAsync(x=>x.Id==id);
        return movie;
    }
}
