using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashBer.Models;
using CarWashBer.Services;
using CarWashBer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarWashBer.Controllers
{
    public class AccountController : Controller
    {
        private IManageCars _manageCars;
        private IManageAccount _manageAccount;
        private readonly UserManager<Customer> _userManager;
        private readonly SignInManager<Customer> _signInManager;

        public AccountController(UserManager<Customer> userManager, 
                                 SignInManager<Customer> signInManager,
                                 IManageCars manageCars,
                                 IManageAccount manageAccount)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._manageCars = manageCars;
            this._manageAccount = manageAccount;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.ListOfBrands = _manageCars.GetCarBrands();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {                
                var user = new Customer { UserName=registerViewModel.Email, Email = registerViewModel.Email  };
                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                var result2 = await _userManager.AddToRoleAsync(user, "user");
                if (result.Succeeded&&result2.Succeeded)
                {                    
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var carViewModel = _manageCars.ConvertToCarViewModel(registerViewModel);
                    _manageCars.AddCar(user, carViewModel);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password,
                                                                     loginViewModel.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))                    
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(loginViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetModelsList(int id)
        {
            var CarModelsList = new SelectList(_manageCars.GetCarModelsByCarBrandId(id), "CarModelId", "ModelName");
            return Json(CarModelsList);
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await _userManager.ChangePasswordAsync(user,
                    changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("index", "home");
            }

            return View(changePasswordViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }
            else
            {
                await _signInManager.SignOutAsync();
                _manageAccount.DeleteUserCarsAndReservations(user);
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("index","home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return RedirectToAction("index","home");
            }
        }

    }
}