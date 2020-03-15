using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public Car Car { get; set; }
        public Gate Gate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Cost { get; set; }
        public List<OperationReservation> OperationReservations { get; set; }
    }
}
