using eTickets.Data;
using eTickets.Data.Services;
using eTickets.IdentityEntities;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;
        //
        private readonly AppDbContext _context;

        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager,AppDbContext appDbContext)
        {
            _cartService = cartService;
            _userManager = userManager;
            _context = appDbContext;
        }

        public async Task<IActionResult> Index(int id)
        {
            // Get the current user object
            var currentUser = await _userManager.GetUserAsync(User);
            // Get the user ID
            var userId = currentUser.Id;
            //
            
/*            var cart = await _cartService.GetCartByUserIdAsync(userId);
*/
            Cart cart = await _context.Cats.FirstOrDefaultAsync(c=>c.UserId==userId);
            if (cart == null)
            {
                Cart cart1 = new Cart
                {
                    UserId = userId

                };
                await _cartService.CreateCartAsync(cart1);

                return View(cart1);
            }

            return View(cart);
        }


    }
}
