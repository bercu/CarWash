using CarWashBer.DAL;
using CarWashBer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public class HelpNewReservation:IHelpNewReservation
    {

        private int TotalTime(Reservation reservation)
        {
            int total = 0;
            foreach(var operationReservation in reservation.OperationReservations)
            {
                total = total + operationReservation.Operation.TimeSpent;
            }
            return total;
        }

        private DateTime CalculateEndingTime(Reservation reservation)
        {
            DateTime ending = reservation.StartTime.AddMinutes(TotalTime(reservation));
            return ending;
        }

        private double TotalCost(Reservation reservation)
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
            if (gate.Reservations == null)
                return true;
            else
            {
                foreach (var reservation in gate.Reservations)
                {
                    if ((beginning > reservation.StartTime && beginning < reservation.EndTime) ||
                       (ending > reservation.StartTime && ending < reservation.EndTime))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private Gate SelectGate (List<Gate> gates, DateTime beginning, DateTime ending)
        {
            foreach(var gate in gates)
            {
                if (GateIsFree(gate, beginning, ending))
                    return gate;
            }
            return null;
        }

        public bool SetUpReservation(Reservation reservation, List<Gate> gates)
        {
            reservation.Cost = TotalCost(reservation);
            reservation.EndTime = CalculateEndingTime(reservation);
            var auxGate = SelectGate(gates, reservation.StartTime, reservation.EndTime);
            if (auxGate != null)
                reservation.Gate = auxGate;
            else
                return false;
            return true;
        }

    }
}
