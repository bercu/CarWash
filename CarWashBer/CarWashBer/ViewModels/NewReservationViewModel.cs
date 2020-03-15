using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.ViewModels
{
    public class NewReservationViewModel
    {
        [Required]
        public string Username { get; set; }
        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; }
    }
}
