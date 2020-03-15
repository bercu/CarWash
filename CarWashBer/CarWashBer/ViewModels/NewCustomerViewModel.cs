using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.ViewModels
{
    public class NewCustomerViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Display(Name ="Car Brand")]
        public string CarBrand { get; set; }
        [Display(Name="Car Model")]
        public string CarModel { get; set; }
        [Required]
        [Display(Name="License Plate")]
        public string LicensePlate { get; set; }
    }
}
