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

        public Customer CreateNewCustomer(NewCustomerViewModel newCustomerViewModel)
        {
            Customer customer = new Customer();
            customer.UserName = newCustomerViewModel.Username;
            customer.Name = newCustomerViewModel.Name;

            Car car = new Car();
            car.Brand = newCustomerViewModel.CarBrand;
            car.Model = newCustomerViewModel.CarModel;
            car.LicensePlate = newCustomerViewModel.LicensePlate;
            car.Customer = customer;

            _unitOfWork.CustomerRepository.Insert(customer);
            _unitOfWork.CarRepository.Insert(car);
            _unitOfWork.Commit();

            return customer;
        }
    }
}
