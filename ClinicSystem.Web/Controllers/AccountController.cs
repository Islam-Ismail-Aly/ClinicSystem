using ClinicSystem.Core.Entities;
using ClinicSystem.Core.Interfaces;
using ClinicSystem.Web.Utilities;
using ClinicSystem.Web.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork<Secretary> _secretaryUnitOfWork;
        private readonly IUnitOfWork<Doctor> _doctorUnitOfWork;
        
        public AccountController(RoleManager<IdentityRole> RoleManager, UserManager<ApplicationUser> UserManager,
            SignInManager<ApplicationUser> signInManager, IUnitOfWork<Secretary> secretaryUnitOfWork, 
            IUnitOfWork<Doctor> doctorUnitOfWork)
        {
            _roleManager = RoleManager;
            _userManager = UserManager;
            _signInManager = signInManager;
            _secretaryUnitOfWork = secretaryUnitOfWork;
            _doctorUnitOfWork = doctorUnitOfWork;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            // Check if the provided model is valid
            if (ModelState.IsValid)
            {
                // Find the user by email
                ApplicationUser user = await _userManager.FindByNameAsync(viewModel.Email);

                // if user exist
                if (user != null)
                {
                    // Check if the provided password matches the user's password
                    bool isValidPassword = await _userManager.CheckPasswordAsync(user, viewModel.Password);

                    if (isValidPassword)
                    {
                        // Sign in the user
                        await _signInManager.SignInAsync(user, viewModel.RememberMe);

                        // Redirect to home index upon successful login
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Json(new { success = false, message = "Incorrect Email or Password!" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Email not found" });
                }
            }

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    UserName = viewModel.Email,
                    Address = viewModel.Address
                };

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                // If user creation is successful
                if (result.Succeeded)
                {
                    if (viewModel.Roles.ToString() == Common.SecretaryRole)
                    {
                        var secretary = new Secretary
                        {
                            FirstName = viewModel.FirstName,
                            LastName = viewModel.LastName,
                            ApplicationUserId = user.Id,
                        };

                        await _secretaryUnitOfWork.Entity.InsertAsync(secretary);

                        await _secretaryUnitOfWork.SaveAsync();

                        await _userManager.AddToRoleAsync(user, viewModel.Roles.ToString());

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return Json(new { success = true }); 
                    }

                    if (viewModel.Roles.ToString() == Common.DoctorRole)
                    {
                        var doctor = new Doctor
                        {
                            FirstName = viewModel.FirstName,
                            LastName = viewModel.LastName,
                            ApplicationUserId = user.Id,
                        };

                        await _doctorUnitOfWork.Entity.InsertAsync(doctor);

                        await _doctorUnitOfWork.SaveAsync();

                        await _userManager.AddToRoleAsync(user, viewModel.Roles.ToString());

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return Json(new { success = true });
                    }
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description);
                    return Json(new { success = false, message = string.Join(", ", errors) });
                }
            }

            return Json(new { success = false, message = "Invalid registration data!" });
        }

        //[HttpGet]
        //public IActionResult Users()
        //{
        //    return View(_vwUsers.Entity.GetAll().Where(vw => vw.Status));
        //}

        //public async Task<IActionResult> DeleteUser(string userId)
        //{
        //    var existingCustomer = _customerUnitOfWork.UserRepository.GetByUserId(userId);

        //    if (existingCustomer == null)
        //    {
        //        return NotFound(new { message = "Customer not found." });
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound(new { message = "User not found." });
        //    }

        //    var result = await _userManager.DeleteAsync(user);
        //    if (!result.Succeeded)
        //    {
        //        // Construct error message
        //        var errorMessage = string.Join("\n", result.Errors.Select(e => e.Description));
        //        return BadRequest(new { message = errorMessage });
        //    }

        //    return RedirectToAction(nameof(Users));
        //}

        //[HttpGet]
        //public async Task<IActionResult> Profile()
        //{
        //    var currentUser = await _userManager.GetUserAsync(User);
        //    if (currentUser != null)
        //    {
        //        var customer = _customerUnitOfWork.UserRepository.GetByUserId(currentUser.Id);

        //        if (customer != null)
        //        {
        //            ViewBag.Phone = customer.Phone;
        //            ViewBag.City = customer.City;
        //            ViewBag.DateAdded = customer.CreationDate.ToShortDateString();
        //            ViewBag.Status = customer.Status;
        //        }
        //    }
        //    return View();
        //}
    }
}
