using CarWashBer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public interface IManageAccount
    {
        void DeleteUserCarsAndReservations(Customer user);
    }
}
