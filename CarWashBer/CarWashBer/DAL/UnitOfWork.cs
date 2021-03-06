﻿using CarWashBer.Models;
using CarWashBer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarWashContext _context;

        public IGenericRepository<Customer> CustomerRepository { get; }
        public IGenericRepository<Car> CarRepository { get; }
        public IGenericRepository<Operation> OperationRepository { get; }
        public IGenericRepository<Gate> GateRepository { get; }
        public IGenericRepository<Reservation> ReservationRepository { get; }
        public IGenericRepository<CarBrand> CarBrandRepository { get; }
        public IGenericRepository<CarModel> CarModelRepository { get; }
        public UnitOfWork(CarWashContext context)
        {
            _context = context;
            CustomerRepository = new GenericRepository<Customer>(context);
            CarRepository = new GenericRepository<Car>(context);
            OperationRepository = new GenericRepository<Operation>(context);
            GateRepository = new GenericRepository<Gate>(context);
            ReservationRepository = new GenericRepository<Reservation>(context);
            CarBrandRepository = new GenericRepository<CarBrand>(context);
            CarModelRepository = new GenericRepository<CarModel>(context);
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();

        }
    }
}
