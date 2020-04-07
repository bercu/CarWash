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

namespace CarWashBer.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private INewCustomer _newCustomer;
        private readonly UserManager<Customer> userManager;
        private readonly SignInManager<Customer> signInManager;

        public AccountController(INewCustomer newCustomer, 
                                 UserManager<Customer> userManager, 
                                 SignInManager<Customer> signInManager)
        {
            this._newCustomer = newCustomer;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {                
                var user = new Customer { UserName=registerViewModel.Email, Email = registerViewModel.Email  };
                var result = await userManager.CreateAsync(user, registerViewModel.Password);
                var result2 = await userManager.AddToRoleAsync(user, "user");
                if (result.Succeeded&&result2.Succeeded)
                {                    
                    await signInManager.SignInAsync(user, isPersistent: false);
                    _newCustomer.RegisterCarForNewCustomer(user,registerViewModel);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LogInViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password,
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


    }
}