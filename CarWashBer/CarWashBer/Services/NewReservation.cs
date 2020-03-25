using CarWashBer.DAL;
using CarWashBer.Models;
using CarWashBer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public class NewReservation:INewReservation
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHelpNewReservation _helpNewReservation;

        public NewReservation(IUnitOfWork unitOfWork, IHelpNewReservation helpNewReservation)
        {
            _unitOfWork = unitOfWork;
            _helpNewReservation = helpNewReservation;
        }

        private Car FindTheCar(NewReservationViewModel newReservationViewModel)
        {            
            var cars = _unitOfWork.CarRepository.GetAll().ToList();
            var car = cars.FirstOrDefault(aux => aux.LicensePlate == newReservationViewModel.LicensePlate);
            if (car == null)
                return null;
            return car;
        }
        
        private List<Operation> SearchOperation(NewReservationViewModel newReservationViewModel)
        {
            var operations = new List<Operation>();
            foreach(var operation in newReservationViewModel.Operations)
            {
                if (operation.IsChecked)
                {
                    var auxOperation = _unitOfWork.OperationRepository.GetById(operation.OperationId);
                    operations.Add(auxOperation);
                }
            }
            return operations;
        }

        private List<OperationReservation> FindOperationReservations (Reservation reservation, List<Operation> operations)
        {
            var auxOperationReservation = new List<OperationReservation>();
            foreach (var operation in operations)
            {             
                //operation.IsChecked = true;
                var auxOpRes = new OperationReservation();
                auxOpRes.Operation = operation;
                auxOpRes.Reservation = reservation;
                auxOperationReservation.Add(auxOpRes);
            }
            if (auxOperationReservation == null)
                return null;
            return auxOperationReservation;
        }

        public void GetOperationsFromDatabase (NewReservationViewModel newReservationViewModel)
        {
            newReservationViewModel.Operations = _unitOfWork.OperationRepository.GetAll().ToList();
        }

        public void SetUpTheReservation(Reservation reservation, NewReservationViewModel newReservationViewModel)
        {
            reservation.Car = FindTheCar(newReservationViewModel);
            reservation.StartTime = newReservationViewModel.StartTime;
            reservation.OperationReservations = FindOperationReservations(reservation, SearchOperation(newReservationViewModel));
            _helpNewReservation.SetUpReservation(reservation, _unitOfWork.GateRepository.GetAll().ToList());

            _unitOfWork.ReservationRepository.Insert(reservation);
            _unitOfWork.Commit();
        }


    }
}
