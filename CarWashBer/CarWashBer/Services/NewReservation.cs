using CarWashBer.DAL;
using CarWashBer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public class NewReservation
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewReservation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private int TotalTime(Reservation reservation)
        {
            int total = 0;
            foreach(var operationReservation in reservation.OperationReservations)
            {
                total = total + operationReservation.Operation.TimeSpent;
            }
            return total;
        }

        public DateTime CalculateEndingTime(Reservation reservation)
        {
            DateTime ending = reservation.StartTime.AddMinutes(TotalTime(reservation));
            return ending;
        }

        public double TotalCost(Reservation reservation)
        {
            double total = 0;
            foreach (var operationReservation in reservation.OperationReservations)
            {
                total = total + operationReservation.Operation.Cost;
            }
            return total;
        }

        private bool GateIsFree(Gate gate, DateTime beginning, DateTime ending)
        {
            foreach(var reservation in gate.Reservations)
            {
                if((beginning>reservation.StartTime && beginning<reservation.EndTime)||
                   (ending>reservation.StartTime && ending < reservation.EndTime))
                {
                    return false;
                }
            }
            return true;
        }

        public int SelectGate (List<Gate> gates, DateTime beginning, DateTime ending)
        {
            foreach(var gate in gates)
            {
                if (GateIsFree(gate, beginning, ending))
                    return gate.GateId;
            }
            return -1;
        }

        public void SetUpReservation(Reservation reservation)
        {

        }




}
}
