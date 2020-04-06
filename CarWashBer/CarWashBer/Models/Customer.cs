using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Models
{
    public class Customer :IdentityUser
    {
        public List<Car> Cars { get; set; }
    }
}
