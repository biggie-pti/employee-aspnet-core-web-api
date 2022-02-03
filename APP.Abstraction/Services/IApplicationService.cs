using APP.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Abstraction.Services
{
    public interface IApplicationService
    {
        Task<List<string>> UpdateDepartment(Department department);
        Task<List<string>> DeleteDepartment(Guid Id);
        Task<Department> GetDepartmentById(Guid Id);
        Task<List<Department>> GetAllDepartments();
        Task<List<string>> CreateDepartment(Department department);


        Task<List<string>> UpdateEmployee(Employee employee);
        Task<List<string>> DeleteEmployee(Guid Id);
        Task<Employee> GetEmployeeById(Guid Id);
        Task<List<Employee>> GetAllEmployees();
        Task<List<string>> CreateEmployee(Employee employee);
    }
}
