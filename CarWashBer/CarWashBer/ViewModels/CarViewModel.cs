using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.ViewModels
{
    public class CarViewModel
    {
        [Display(Name = "Car Brand")]
        public int CarBrandId { get; set; }
        [Display(Name = "Car Model")]
        public int CarModelId { get; set; }
        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; }
    }
}
