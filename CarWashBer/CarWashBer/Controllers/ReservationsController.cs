using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarWashBer.Models;
using CarWashBer.Services;
using Microsoft.AspNetCore.Identity;
using CarWashBer.ViewModels;

namespace CarWashBer.Controllers
{
    public class ReservationsController : Controller
    {
        private IManageReservations _manageReservations;
        private IManageCars _manageCars;
        private readonly UserManager<Customer> _userManager;

        public ReservationsController(IManageReservations manageReservations, UserManager<Customer> userManager, IManageCars manageCars)
        {
            this._manageReservations = manageReservations;
            this._userManager = userManager;
            this._manageCars = manageCars;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(_manageReservations.GetUserReservations(user));
        }

        public IActionResult Details(int? id)
        {
            var reservation = _manageReservations.GetReservationById(id);
            return View(reservation);
        }

        public async Task<IActionResult> Create()
        {
            var auxViewModel = new NewReservationViewModel();
            _manageReservations.GetOperationsFromDatabase(auxViewModel);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.CarsList = _manageCars.GetUserCars(user);
            return View(auxViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewReservationViewModel newReservationViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var reservation = new Reservation();
                _manageReservations.AddReservation(newReservationViewModel, reservation);
                return RedirectToAction(nameof(Index));
            }
            return View(newReservationViewModel);
        }

        public IActionResult Edit(int? id)
        {
            var reservation = _manageReservations.GetReservationById(id);
            return View(reservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ReservationId,StartTime,EndTime,Cost")] Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _manageReservations.UpdateReservation(id, reservation);
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        public IActionResult Delete(int? id)
        {
            var reservation = _manageReservations.GetReservationById(id);
            return View(reservation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _manageReservations.DeleteReservationById(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
