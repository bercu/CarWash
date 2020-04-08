using CarWashBer.Models;
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
        [Display(Name = "License Plate")]
        public int CarId { get; set; }

        [Required]
        public List<Operation> Operations { get; set; } 

        [Required]
        [Display(Name ="Date and Time")]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:yyyy-MM-dd HH:mm}")]
        public DateTime StartTime { get; set; }
    }
}
