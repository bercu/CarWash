using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarWashBer.Models;
using CarWashBer.Services;

namespace CarWashBer.Controllers
{
    public class CarsController : Controller
    {
        private IManageCars _newManagedCar;

        public CarsController(IManageCars newManagedCar)
        {
            this._newManagedCar = newManagedCar;
        }

        public IActionResult Index()
        {
            return View(_newManagedCar.GetCars());
        }

        public IActionResult Details(int? id)
        {
            var car = _newManagedCar.GetCarById(id);
            return View(car);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CarId,Brand,Model,LicensePlate")] Car car)
        {
            if (ModelState.IsValid)
            {
                _newManagedCar.AddCar(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public IActionResult Edit(int? id)
        {
            var car = _newManagedCar.GetCarById(id);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CarId,Brand,Model,LicensePlate")] Car car)
        {
            if (ModelState.IsValid)
            {
                if(_newManagedCar.CanEditCar(id,car))
                    return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public IActionResult Delete(int? id)
        {
            var car = _newManagedCar.GetCarById(id);

            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _newManagedCar.DeleteCarById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
