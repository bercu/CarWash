using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Models
{
    public class CarBrand
    {
        public int CarBrandId { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; }
        public List<CarModel> CarModels { get; set; }
    }
}
