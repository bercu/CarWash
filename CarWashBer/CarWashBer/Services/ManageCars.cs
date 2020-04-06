using CarWashBer.DAL;
using CarWashBer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public class ManageCars:IManageCars
    {

        private readonly IUnitOfWork _unitOfWork;

        public ManageCars(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private bool CarExists(int id)
        {
            if (GetCarById(id) == null)
                return false;
            else
                return true;
        }

        public List<Car> GetCars()
        {
            return _unitOfWork.CarRepository.GetAll().ToList();
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

        public void AddCar(Car car)
        {
            _unitOfWork.CarRepository.Insert(car);
            _unitOfWork.Commit();
        }


        public bool CanEditCar(int id, Car car)
        {
            if (id != car.CarId)
            {
                return false;
            }
            if (CarExists(car.CarId)) {
                _unitOfWork.CarRepository.Update(car);
                _unitOfWork.CarRepository.Save();
                return true;
            }
            return false;
        }

        public void DeleteCarById(int id)
        {
            var car = GetCarById(id);
            _unitOfWork.CarRepository.Delete(car);
            _unitOfWork.CarRepository.Save();
        }
    }
}