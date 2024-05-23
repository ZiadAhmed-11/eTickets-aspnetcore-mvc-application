using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAll();
            return View(allMovies);
        }

        //Get: Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _service.GetById(id);
            if (movieDetails == null)
                return RedirectToAction(nameof(Index));
            return View(movieDetails);
        }

        //Get : Movies/create
        public async Task<IActionResult> Create()
        {
            ViewData["Cinemas"] = new SelectList(await _service.GetCinemasAsync(), "Id", "Name");
            ViewData["Producers"] = new SelectList(await _service.GetProducersAsync(), "Id", "FullName");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,ImageURL,StartDate,EndDate,MovieCategory,CinemaId,ProducerId")] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            await _service.Add(movie);
            ViewData["Cinemas"] = new SelectList(await _service.GetCinemasAsync(), "Id", "Name", movie.CinemaId);
            ViewData["Producers"] = new SelectList(await _service.GetProducersAsync(), "Id", "FullName", movie.ProducerId);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int? id)
        {
            var movie = await _service.GetById(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            ViewData["Cinemas"] = new SelectList(await _service.GetCinemasAsync(), "Id", "Name", movie.CinemaId);
            ViewData["Producers"] = new SelectList(await _service.GetProducersAsync(), "Id", "FullName", movie.ProducerId);

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ImageURL,StartDate,EndDate,MovieCategory,CinemaId,ProducerId")] Movie movie)
        {

            if (ModelState.IsValid)
            {
                
                    await _service.Update(id, movie);
                
                return RedirectToAction(nameof(Index));
            }

            ViewData["Cinemas"] = new SelectList(await _service.GetCinemasAsync(), "Id", "Name", movie.CinemaId);
            ViewData["Producers"] = new SelectList(await _service.GetProducersAsync(), "Id", "FullName", movie.ProducerId);

            return View(movie);
        }
    }
}
