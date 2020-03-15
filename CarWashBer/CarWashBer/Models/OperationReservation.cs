using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Models
{
    public class OperationReservation
    {
        public int OperationId { get; set; }
        public Operation Operation { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
