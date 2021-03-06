﻿using System;
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
    public class CarsController : Controller
    {
        private IManageCars _manageCars;
        private readonly UserManager<Customer> _userManager;

        public CarsController(IManageCars manageCars,
                              UserManager<Customer> userManager)
        {
            this._manageCars = manageCars;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(_manageCars.GetUserCars(user));
        }

        public IActionResult Details(int? id)
        {
            var car = _manageCars.GetCarById(id);
            return View(car);
        }

        public IActionResult Create()
        {
            ViewBag.ListOfBrands = _manageCars.GetCarBrands();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarViewModel carViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                _manageCars.AddCar(user,carViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(carViewModel);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.ListOfBrands = _manageCars.GetCarBrands();
            var car = _manageCars.GetCarById(id);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CarId,Brand,Model,LicensePlate")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _manageCars.UpdateCar(id, car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public IActionResult Delete(int? id)
        {
            var car = _manageCars.GetCarById(id);
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _manageCars.DeleteCarById(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public JsonResult GetModelsList(int id)
        {
            var CarModelsList = new SelectList(_manageCars.GetCarModelsByCarBrandId(id), "CarModelId", "ModelName");
            return Json(CarModelsList);
        }
    }
}
