using CarWashBer.DAL;
using CarWashBer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public class ManageCars : IManageCars
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Customer> _userManager;

        public ManageCars(IUnitOfWork unitOfWork, 
                          UserManager<Customer> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IEnumerable<Car> GetCars()
        {
            return _unitOfWork.CarRepository.GetAll().ToList();
        }

        public IEnumerable<Car> GetUserCars(Customer customer)
        {            
            return _unitOfWork.CarRepository.GetAll().Where(x=>x.Customer==customer).ToList();
        }

        public Car GetCarById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var car = _unitOfWork.CarRepository.GetById(id);

            if (car == null)
            {
                return null;
            }
            return car;
        }

        public void AddCar(Car car,Customer customer)
        {
            car.Customer = customer;
            _unitOfWork.CarRepository.Insert(car);
            _unitOfWork.Commit();
        }

        public void DeleteCarById(int id)
        {
            foreach(var reservation in _unitOfWork.ReservationRepository.GetAll().ToList())
            {
                if (reservation.Car!=null && reservation.Car.CarId == id)
                    _unitOfWork.ReservationRepository.Delete(reservation.ReservationId);
            }
            _unitOfWork.CarRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public void UpdateCar(int id, Car car)
        {
            _unitOfWork.CarRepository.Update(car);
            _unitOfWork.Commit();
        }
    }
}