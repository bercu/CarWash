using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password",ErrorMessage = "Password and confirmation password don't match")]
        public string ConfirmPassword { get; set; }
        [Display(Name ="Car Brand")]
        public string CarBrand { get; set; }
        [Display(Name="Car Model")]
        public string CarModel { get; set; }
        [Required]
        [Display(Name="License Plate")]
        public string LicensePlate { get; set; }
    }
}
