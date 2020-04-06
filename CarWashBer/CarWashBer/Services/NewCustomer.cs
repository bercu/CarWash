using CarWashBer.DAL;
using CarWashBer.Models;
using CarWashBer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public class NewCustomer:INewCustomer
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewCustomer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Customer RegisterCustomer(RegisterViewModel registerViewModel)
        {
            Customer customer = new Customer();
            customer.Email = registerViewModel.Email;

            Car car = new Car();
            car.Brand = registerViewModel.CarBrand;
            car.Model = registerViewModel.CarModel;
            car.LicensePlate = registerViewModel.LicensePlate;
            car.Customer = customer;

            _unitOfWork.CustomerRepository.Insert(customer);
            _unitOfWork.CarRepository.Insert(car);
            _unitOfWork.Commit();

            return customer;
        }
    }
}
