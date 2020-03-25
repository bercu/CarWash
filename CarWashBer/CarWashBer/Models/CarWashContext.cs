using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Models
{
    public class CarWashContext:DbContext
    {
        public CarWashContext(DbContextOptions<CarWashContext> options)
    : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<OperationReservation> OperationReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationReservation>().HasKey(sc => new { sc.OperationId, sc.ReservationId });
        }
    }
}
