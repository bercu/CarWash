using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Models
{
    public class Operation
    {
        [Key]
        public int OperationId { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public int TimeSpent { get; set; }
        public bool IsChecked { get; set; }
        public List<OperationReservation> OperationReservations { get; set; }
    }
}
