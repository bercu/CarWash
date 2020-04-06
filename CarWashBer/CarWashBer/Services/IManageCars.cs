using CarWashBer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public interface IManageCars
    {

        List<Car> GetCars();

        Car GetCarById(int? id);

        void AddCar(Car car);

        bool CanEditCar(int id, Car car);

        void DeleteCarById(int id);
    }
}
