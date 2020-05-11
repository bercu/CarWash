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
    public class ManageReservations:IManageReservations
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INewReservation _newReservation;
        private readonly UserManager<Customer> _userManager;

        public ManageReservations(IUnitOfWork unitOfWork,
                                  UserManager<Customer> userManager,
                                  INewReservation newReservation)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _newReservation = newReservation;
        }

        public IEnumerable<Reservation> GetReservations()
        {
            return _unitOfWork.ReservationRepository.GetAll().ToList();
        }

        private IEnumerable<Reservation> GetUserReservations(Customer customer, List<Reservation> reservations)
        {            
            var cars = _unitOfWork.CarRepository.GetAll().Where(x => x.Customer == customer).ToList();
            var userReservations = new List<Reservation>();
            foreach(var reservation in reservations)
            {
                if (cars.Any(x => x == reservation.Car))
                    userReservations.Add(reservation);
            }
            return userReservations;
        }

        public IEnumerable<Reservation> GetUserFutureReservations(Customer customer)
        {
            var reservations = _unitOfWork.ReservationRepository.GetAll().Where(x => x.EndTime > DateTime.Now).ToList();
            return GetUserReservations(customer, reservations);
        }

        public IEnumerable<Reservation> GetUserOldReservations(Customer customer)
        {
            var reservations = _unitOfWork.ReservationRepository.GetAll().Where(x => x.EndTime <= DateTime.Now).ToList();
            return GetUserReservations(customer, reservations);
        }

        public Reservation GetReservationById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var reservation = _unitOfWork.ReservationRepository.GetById(id);

            if (reservation == null)
            {
                return null;
            }
            return reservation;
        }

        public void AddReservation(NewReservationViewModel newReservationViewModel, Reservation reservation)
        {
            _newReservation.SetUpTheReservation(reservation, newReservationViewModel);
        }

        public void DeleteReservationById(int id)
        {
            _unitOfWork.ReservationRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public void UpdateReservation(int id, Reservation reservation)
        {
            _unitOfWork.ReservationRepository.Update(reservation);
            _unitOfWork.Commit();
        }

        public void GetOperationsFromDatabase(NewReservationViewModel newReservationViewModel)
        {
            newReservationViewModel.Operations = _unitOfWork.OperationRepository.GetAll().ToList();
        }

    }
}
