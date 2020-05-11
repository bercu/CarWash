using CarWashBer.DAL;
using CarWashBer.Models;
using CarWashBer.ViewModels;
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

        public ManageCars(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Car> GetCars()
        {
            return _unitOfWork.CarRepository.GetAll().ToList();
        }

        public IEnumerable<CarBrand> GetCarBrands()
        {
            var ListOfCarBrands = _unitOfWork.CarBrandRepository.GetAll().ToList();
            ListOfCarBrands.Insert(0, new CarBrand { CarBrandId = 0, BrandName = "Select" });
            return ListOfCarBrands;
        }

        private void SelectModelsForEachBrand(List<CarBrand> ListOfCarBrands)
        {
            foreach (var CarBrand in ListOfCarBrands)
            {
                var ListOfCarModels = _unitOfWork.CarModelRepository.GetAll().Where(x => x.CarBrand.CarBrandId == CarBrand.CarBrandId).ToList();
                CarBrand.CarModels = ListOfCarModels;
            }
        }

        public IEnumerable<CarModel> GetCarModelsByCarBrandId (int id)
        {
            SelectModelsForEachBrand(_unitOfWork.CarBrandRepository.GetAll().ToList());
            var auxCarBrand = _unitOfWork.CarBrandRepository.GetById(id);
            return auxCarBrand.CarModels;
        }

        public IEnumerable<Car> GetUserCars(Customer customer)
        {
            var userCars = _unitOfWork.CarRepository.GetAll().Where(x => x.Customer == customer).ToList();
            var carModels = _unitOfWork.CarModelRepository.GetAll().Where(x=>x.Cars == userCars).ToList();
            var carBrands = _unitOfWork.CarBrandRepository.GetAll().ToList();
            return userCars;
        }

        public Car GetCarById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var car = _unitOfWork.CarRepository.GetById(id);
            var carModels = _unitOfWork.CarModelRepository.GetAll().ToList();
            var carBrands = _unitOfWork.CarBrandRepository.GetAll().ToList();

            if (car == null)
            {
                return null;
            }
            return car;
        }

        public CarViewModel ConvertToCarViewModel(RegisterViewModel registerViewModel)
        {
            CarViewModel carViewModel = new CarViewModel();
            carViewModel.CarBrandId = registerViewModel.CarBrandId;
            carViewModel.CarModelId = registerViewModel.CarModelId;
            carViewModel.LicensePlate = registerViewModel.LicensePlate;
            return carViewModel;
        }

        private Car MapCarFromViewModel(CarViewModel carViewModel)
        {
            var car = new Car();
            car.LicensePlate = carViewModel.LicensePlate;
            var carBrand = _unitOfWork.CarBrandRepository.GetAll().FirstOrDefault(x => x.CarBrandId == carViewModel.CarBrandId);
            var carModel = _unitOfWork.CarModelRepository.GetAll().FirstOrDefault(x => x.CarModelId == carViewModel.CarModelId);
            if (carBrand != null && carModel != null)
            {
                car.CarModel = carModel;
                car.CarModel.CarBrand = carBrand;                
                return car;
            }
            else
                return null;
        }

        public void AddCar(Customer customer,CarViewModel carViewModel)
        {
            var car = MapCarFromViewModel(carViewModel);
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