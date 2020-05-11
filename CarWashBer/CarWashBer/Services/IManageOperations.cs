using CarWashBer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWashBer.Services
{
    public interface IManageOperations
    {
        IEnumerable<Operation> GetOperations();

        Operation GetOperationById(int? id);

        void AddOperation(Operation operation);

        void DeleteOperationById(int id);

        void UpdateOperation(int id, Operation operation);
    }
}
