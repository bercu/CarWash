using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Models
{
    public class Gate
    {
        [Key]
        public int GateId { get; set; }
        public int GateNumber { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
