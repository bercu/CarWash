using CarWashBer.Models;
using CarWashBer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public interface INewReservation
    {
        void GetOperationsFromDatabase(NewReservationViewModel newReservationViewModel);
        void SetUpTheReservation(Reservation reservation, NewReservationViewModel newReservationViewModel);
    }
}
