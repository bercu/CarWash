using CarWashBer.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CarWashBer.Services
{
    public interface IManageCars
    {

        IEnumerable<Car> GetCars();

        IEnumerable<Car> GetUserCars(Customer customer);

        Car GetCarById(int? id);

        void AddCar(Car car,Customer customer);

        void DeleteCarById(int id);

        void UpdateCar(int id, Car car);
                
    }
}
