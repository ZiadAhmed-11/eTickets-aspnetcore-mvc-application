using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [AllowAnonymous]

    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService actorsService)
        {
            _service = actorsService;
        }
        public async Task<IActionResult> Index()
        {
            var data=await _service.GetAll();
            return View(data);
        }
        //Get : Actors/create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor)
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.Add(actor);
            return RedirectToAction(nameof(Index));

        }

        //Get: Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null)
                return RedirectToAction(nameof(Index));
            return View(actorDetails);
        }


        //Get: Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null)
                return RedirectToAction(nameof(Index));
            return View(actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.Update(id, actor);
            return RedirectToAction(nameof(Index));
        }

        //Get: Actors/Edit/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetById(id);
            if (actorDetails == null)
                return RedirectToAction(nameof(Index));
            return View(actorDetails);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails= await _service.GetById(id);
            if (actorDetails == null)
                return RedirectToAction(nameof(Index));

            await _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
