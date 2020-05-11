using CarWashBer.DAL;
using CarWashBer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public class ManageAccount:IManageAccount
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IManageCars _manageCars;

        public ManageAccount(IUnitOfWork unitOfWork, IManageCars manageCars)
        {
            _unitOfWork = unitOfWork;
            _manageCars = manageCars;
        }

        public void DeleteUserCarsAndReservations(Customer user)
        {
            var cars = _unitOfWork.CarRepository.GetAll().Where(x=>x.Customer==user).ToList();
            foreach(var car in cars)
            {
                _manageCars.DeleteCarById(car.CarId);
            }
        }




    }
}
