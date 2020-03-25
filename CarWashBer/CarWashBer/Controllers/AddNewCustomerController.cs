using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashBer.DAL;
using CarWashBer.Services;
using CarWashBer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarWashBer.Controllers
{
    public class AddNewCustomerController : Controller
    {
        private INewCustomer _newCustomer;

        public AddNewCustomerController(INewCustomer newCustomer)
        {
            this._newCustomer = newCustomer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewCustomerViewModel newCustomerViewModel)
        {
            if (ModelState.IsValid)
            {
                var customer = _newCustomer.CreateNewCustomer(newCustomerViewModel);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}