using CarWashBer.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public class NewReservation
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewReservation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


    }
}
