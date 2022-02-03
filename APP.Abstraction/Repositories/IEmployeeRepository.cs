using APP.Abstraction.Entities;
using APP.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Abstraction.Repositories
{
    public interface IEmployeeRepository
    {
        void UpdateEmployee(EmployeeEntity entity);
        void DeleteEmployee(Guid Id);
        Task<EmployeeEntity> GetEmployeeById(Guid Id);
        Task<List<EmployeeEntity>> GetAllEmployees();
        Task<Guid> CreateEmployee(EmployeeEntity employee);
    }
}
