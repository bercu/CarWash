using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashBer.DAL;
using CarWashBer.Models;
using CarWashBer.Services;
using CarWashBer.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarWashBer.Controllers
{
    public class AddNewReservationController : Controller
    {
        private INewReservation _newReservation;
        private IManageCars _manageCars;
        private readonly UserManager<Customer> _userManager;

        public AddNewReservationController(INewReservation newReservation, UserManager<Customer> userManager, IManageCars manageCars)
        {
            this._newReservation = newReservation;
            this._userManager = userManager;
            this._manageCars = manageCars;
        }
        public async Task<IActionResult> Index()
        {
            var auxViewModel = new NewReservationViewModel();
            _newReservation.GetOperationsFromDatabase(auxViewModel);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CarsList = _manageCars.GetUserCars(user);
            return View(auxViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewReservationViewModel newReservationViewModel)
        {
            if (ModelState.IsValid)
            {
                var reservation = new Reservation();
                _newReservation.SetUpTheReservation(reservation,newReservationViewModel);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}