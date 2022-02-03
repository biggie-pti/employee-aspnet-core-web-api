using APP.Abstraction.Entities;
using APP.Abstraction.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Core.Aggregates
{
   public class EmployeeAggregate : BaseAggregate<Abstraction.Entities.EmployeeEntity>
    {

        public EmployeeAggregate(Abstraction.Entities.EmployeeEntity entity) : base(entity)
        {

        }

        public void SaveEmployee(Employee employee)
        {
            SetEmployeeDetails(employee);
        }

        public void SetEmployeeDetails(Employee employee)
        {
            Entity.Id = employee.Id;
            Entity.EmployeeName = employee.EmployeeName;
            Entity.DateOfJoining = employee.DateOfJoining;
            Entity.Department = employee.Department;
            Entity.PhotoFileName = employee.PhotoFileName;

        }


        public void DeleteEmployee()
        {
            Entity.IsDeleted = true;
        }

        public void ValidateEmployee(Employee employee)
        {

        }

    }
}
