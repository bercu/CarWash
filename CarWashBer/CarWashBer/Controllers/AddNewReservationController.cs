using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashBer.DAL;
using CarWashBer.Models;
using CarWashBer.Services;
using CarWashBer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarWashBer.Controllers
{
    public class AddNewReservationController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private INewReservation _newReservation;

        public AddNewReservationController(IUnitOfWork unitOfWork, INewReservation newReservation)
        {
            this._unitOfWork = unitOfWork;
            this._newReservation = newReservation;
        }
        public IActionResult Index()
        {
            var auxViewModel = new NewReservationViewModel();
            auxViewModel.Operations = _unitOfWork.OperationRepository.GetAll().ToList();
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