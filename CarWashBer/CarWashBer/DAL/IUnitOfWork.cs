using CarWashBer.Models;
using CarWashBer.Repositories;
using CarWashBer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<Car> CarRepository { get; }
        IGenericRepository<Operation> OperationRepository { get; }
        IGenericRepository<Gate> GateRepository { get; }
        IGenericRepository<Reservation> ReservationRepository { get; }
        IGenericRepository<CarBrand> CarBrandRepository { get; }
        IGenericRepository<CarModel> CarModelRepository { get; }
        void Commit();
    }
}
