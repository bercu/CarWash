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
    public class OperationsController : Controller
    {
        private IManageOperations _manageOperations;

        public OperationsController(IManageOperations manageOperations)
        {
            _manageOperations = manageOperations;
        }

        public IActionResult Index()
        {
            return View(_manageOperations.GetOperations());
        }

        public IActionResult Details(int? id)
        {
            var operation = _manageOperations.GetOperationById(id);
            return View(operation);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OperationId,Name,Cost,TimeSpent,IsChecked")] Operation operation)
        {
            if (ModelState.IsValid)
            {
                _manageOperations.AddOperation(operation);
                return RedirectToAction(nameof(Index));
            }
            return View(operation);
        }

        public IActionResult Edit(int? id)
        {
            var operation = _manageOperations.GetOperationById(id);
            return View(operation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("OperationId,Name,Cost,TimeSpent,IsChecked")] Operation operation)
        {
            if (id != operation.OperationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _manageOperations.UpdateOperation(id, operation);
                return RedirectToAction(nameof(Index));
            }
            return View(operation);
        }

        public IActionResult Delete(int? id)
        {
            var operation = _manageOperations.GetOperationById(id);
            return View(operation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _manageOperations.DeleteOperationById(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
