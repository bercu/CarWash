﻿using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarWashBer.Models;

namespace CarWashBer.Models
{
    public class CarWashContext:IdentityDbContext<Customer>
    {
        public CarWashContext(DbContextOptions<CarWashContext> options)
    : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<OperationReservation> OperationReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OperationReservation>().HasKey(sc => new { sc.OperationId, sc.ReservationId });
        }

        public DbSet<CarWashBer.Models.CarModel> CarModel { get; set; }

        public DbSet<CarWashBer.Models.CarBrand> CarBrand { get; set; }
    }
}
