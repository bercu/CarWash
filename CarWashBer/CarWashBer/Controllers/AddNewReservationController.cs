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
    public class AddNewReservationController : Controller
    {
        private IUnitOfWork _unitOfWork;
        //private INewCustomer _newCustomer;

        public AddNewReservationController(IUnitOfWork unitOfWork/*, INewCustomer newCustomer*/)
        {
            this._unitOfWork = unitOfWork;
            //this._newCustomer = newCustomer;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}