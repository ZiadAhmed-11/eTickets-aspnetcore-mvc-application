using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [AllowAnonymous]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAll();
            return View(allCinemas);
        }

        //GET: cinemas/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {

                if (!ModelState.IsValid) return View(cinema);

                await _service.Add(cinema);
                return RedirectToAction(nameof(Index));
            }
            else
                return RedirectToAction(nameof(Index));
            }

        //Get: Cinemas/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetById(id);
            if (cinemaDetails == null)
                return RedirectToAction(nameof(Index));
            return View(cinemaDetails);
        }

        //Get: Cinemas/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {

                var actorDetails = await _service.GetById(id);
                if (actorDetails == null)
                    return RedirectToAction(nameof(Index));
                return View(actorDetails);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.Update(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        //Get: Cinemas/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {

                var cinemaDetails = await _service.GetById(id);
                if (cinemaDetails == null)
                    return RedirectToAction(nameof(Index));
                return View(cinemaDetails);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaDetails = await _service.GetById(id);
            if (cinemaDetails == null)
                return RedirectToAction(nameof(Index));

            await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
