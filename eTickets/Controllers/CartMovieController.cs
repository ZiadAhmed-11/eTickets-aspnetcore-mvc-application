using eTickets.Data.Services;
using eTickets.IdentityEntities;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class CartMovieController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;
        private readonly IMoviesService _movieService;
        private readonly ICartMovieService _cartMovieService;

        public CartMovieController(UserManager<ApplicationUser> userManager,ICartService cartService, ICartMovieService cartMovieService,IMoviesService moviesService)
        {
            _userManager = userManager;
            _cartService = cartService;
            _cartMovieService = cartMovieService;
            _movieService = moviesService;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(int MovieId)
        {
            // Get the current user object
            var currentUser = await _userManager.GetUserAsync(User);
            // Get the user ID
            var userId = currentUser.Id;

            var cart=await _cartService.GetCartByUserIdAsync(userId);
            Movie item = await _movieService.GetAsync(MovieId);
            var price=(Decimal) item.Price;

            CartMovie cartMovie = new CartMovie
            {
                CartId = cart.CartId,
                MovieId = MovieId,
                Quantity = 1,
                Subtotal =  price
            };

            await _cartMovieService.AddCartMovieAsync(cartMovie);
            return RedirectToAction("Index","","Movies");
        }
    }
}
