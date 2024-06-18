using eTickets.Data.Enums;
using eTickets.DTO;
using eTickets.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    [Route("[Controller]/[Action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {

            //check for validation errors
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }

            ApplicationUser user = new ApplicationUser() { Email = registerDTO.Email, PhoneNumber = registerDTO.Phone, UserName = registerDTO.Email, PersonName = registerDTO.PersonName };
            IdentityResult result = await _userManager.CreateAsync(user,registerDTO.Password);
            if (result.Succeeded)
            {
                //check status of radio button
                if(registerDTO.UserType==Data.Enums.UserTypeOptions.Admin)
                {
                    //create (admin) role
                    if(await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.Admin.ToString() };
                        await _roleManager.CreateAsync(applicationRole);

                    }
                    //add new user into 'Admin' role
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.Admin.ToString());
                }
                else
                {
                    //add new user into 'User' role
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());
                }
                //sign in 
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction(nameof(MoviesController.Index), "Movies");
            }
            else
            {
                foreach(IdentityError error in result.Errors) 
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                return View(registerDTO);
            }
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }

            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(MoviesController.Index), "Movies");
            }
            ModelState.AddModelError("Login", "Invalid email or password");
            return View(loginDTO);
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(MoviesController.Index), "Movies");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            var userDetails = new UserDetailsDTO
            {
                Email = currentUser.Email,
                Phone = currentUser.PhoneNumber,
                PersonName = currentUser.PersonName,
            };

            return View(userDetails);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            var editUser = new UserEditDTO
            {
                Email = currentUser.Email,
                Phone = currentUser.PhoneNumber,
                PersonName = currentUser.PersonName
            };

            return View(editUser);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(UserEditDTO editUserDTO)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(temp => temp.Errors).Select(temp => temp.ErrorMessage);
                return View(editUserDTO);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            currentUser.Email = editUserDTO.Email;
            currentUser.PhoneNumber = editUserDTO.Phone;
            currentUser.PersonName = editUserDTO.PersonName;

            if (!string.IsNullOrEmpty(editUserDTO.CurrentPassword) && !string.IsNullOrEmpty(editUserDTO.NewPassword) && editUserDTO.NewPassword == editUserDTO.ConfirmPassword)
            {
                var result = await _userManager.ChangePasswordAsync(currentUser, editUserDTO.CurrentPassword, editUserDTO.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("Edit", error.Description);
                    }
                    return View(editUserDTO);
                }
            }

            var updateResult = await _userManager.UpdateAsync(currentUser);
            if (updateResult.Succeeded)
            {
                return RedirectToAction("Details");
            }

            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError("Edit", error.Description);
            }

            return View(editUserDTO);
        }
    }
}
