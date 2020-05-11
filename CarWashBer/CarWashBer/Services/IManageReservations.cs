using CarWashBer.Models;
using CarWashBer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public interface IManageReservations
    {
        IEnumerable<Reservation> GetReservations();

        IEnumerable<Reservation> GetUserFutureReservations(Customer customer);

        IEnumerable<Reservation> GetUserOldReservations(Customer customer);

        Reservation GetReservationById(int? id);

        void AddReservation(NewReservationViewModel newReservationViewModel, Reservation reservation);

        void DeleteReservationById(int id);

        void UpdateReservation(int id, Reservation reservation);

        void GetOperationsFromDatabase(NewReservationViewModel newReservationViewModel);

    }
}
