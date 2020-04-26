using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Models
{
    public class CarModel
    {
        public int CarModelId { get; set; }
        [Display(Name ="Model")]
        public string ModelName { get; set; }
        public CarBrand CarBrand { get; set; }
        public List<Car> Cars { get; set; }
    }
}
