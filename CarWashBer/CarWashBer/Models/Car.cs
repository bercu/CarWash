﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public Customer Customer { get; set; }
        public CarModel CarModel { get; set; }
        [Display(Name ="License Plate")]
        public string LicensePlate { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
