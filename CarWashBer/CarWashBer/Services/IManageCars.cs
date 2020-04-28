using CarWashBer.Models;
using CarWashBer.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CarWashBer.Services
{
    public interface IManageCars
    {

        IEnumerable<Car> GetCars();
        IEnumerable<CarBrand> GetCarBrands();
        IEnumerable<CarModel> GetCarModelsByCarBrandId(int id);

        IEnumerable<Car> GetUserCars(Customer customer);

        Car GetCarById(int? id);
        CarViewModel ConvertToCarViewModel(RegisterViewModel registerViewModel);

        void AddCar(Customer customer, CarViewModel carViewModel);

        void DeleteCarById(int id);

        void UpdateCar(int id, Car car);
                
    }
}
