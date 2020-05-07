using CarWashBer.DAL;
using CarWashBer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public class ManageOperations:IManageOperations
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Customer> _userManager;

        public ManageOperations(IUnitOfWork unitOfWork,
                          UserManager<Customer> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IEnumerable<Operation> GetOperations()
        {
            return _unitOfWork.OperationRepository.GetAll().ToList();
        }

        public Operation GetOperationById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var operation = _unitOfWork.OperationRepository.GetById(id);

            if (operation == null)
            {
                return null;
            }
            return operation;
        }

        public void AddOperation(Operation operation)
        {
            _unitOfWork.OperationRepository.Insert(operation);
            _unitOfWork.Commit();
        }

        public void DeleteOperationById(int id)
        {
            _unitOfWork.OperationRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public void UpdateOperation(int id, Operation operation)
        {
            _unitOfWork.OperationRepository.Update(operation);
            _unitOfWork.Commit();
        }
    }
}
