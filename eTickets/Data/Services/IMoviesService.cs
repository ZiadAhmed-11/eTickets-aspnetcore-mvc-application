﻿using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetAsync(int id);
        Task<IEnumerable<Cinema>> GetCinemasAsync();
        Task<IEnumerable<Producer>> GetProducersAsync();
    }
}
