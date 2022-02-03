using APP.Abstraction.Entities;
using APP.Abstraction.Models;
using APP.Abstraction.Repositories;
using APP.Abstraction.Services;
using APP.Core.Aggregates;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Core.Services
{
   public class ApplicationService :IApplicationService
    {
        private readonly ILogger<ApplicationService> _logger;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ApplicationService(IDepartmentRepository departmentRepository,IEmployeeRepository employeeRepository, ILogger<ApplicationService> logger)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create component
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public async Task<List<string>> CreateDepartment(Department department)
        {

            _logger.LogInformation("Initialising.....");
           
            var aggregate = new DepartmentAggregate(new DepartmentEntity());
            var result = new List<string>();
            aggregate.ValidateDepartment(department);
            if (aggregate.ResultMessages.Count < 1)
            {
                _logger.LogInformation("save department .....");
                aggregate.SaveDepartment(department);
                await _departmentRepository.CreateDepartment(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;

            }
            return result;

        }


        /// <summary>
        /// Create component
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<List<string>> CreateEmployee(Employee employee)
        {
            _logger.LogInformation("Initialising.....");
            var aggregate = new EmployeeAggregate(new EmployeeEntity());
            var result = new List<string>();
            aggregate.ValidateEmployee(employee);
            if (aggregate.ResultMessages.Count < 1)
            {
                _logger.LogInformation("save department .....");
                aggregate.SaveEmployee(employee);
                await _employeeRepository.CreateEmployee(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;

            }
            return result;

        }


        public async Task<List<string>> DeleteDepartment(Guid Id)
        {
            _logger.LogInformation("Loading department detials......");
            var entity = await _departmentRepository.GetDepartmentById(Id);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("department not found");
                return result;
            }

            var aggregate = new DepartmentAggregate(entity);
            if (aggregate.ResultMessages.Count < 1)
            {
                //save person 
                _logger.LogInformation("Saving department details.....");
                aggregate.DeleteDepartment();
                await _departmentRepository.CreateDepartment (aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;
        }


        public async Task<List<string>> DeleteEmployee(Guid Id)
        {
            _logger.LogInformation("Loading employee detials......");
            var entity = await _employeeRepository.GetEmployeeById(Id);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("employee not found");
                return result;
            }

            var aggregate = new EmployeeAggregate(entity);
            if (aggregate.ResultMessages.Count < 1)
            {
                //save person 
                _logger.LogInformation("Saving employee details.....");
                aggregate.DeleteEmployee();
                await _employeeRepository.CreateEmployee(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;
        }

    

        public async Task<Department> GetDepartmentById(Guid Id)
        {
            var entity = await _departmentRepository.GetDepartmentById(Id);
            var department = new Department(entity);
            return department;
        }

        public async Task<Employee> GetEmployeeById(Guid Id)
        {
            var entity = await _employeeRepository.GetEmployeeById(Id);
            var employee = new Employee(entity);
            return employee;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            var entities = await _departmentRepository.GetAllDepartments();
            var departments = new List<Department>();
            foreach (var entity in entities)
            {
                var department = new Department(entity);
                departments.Add(department);
            }
            return departments ;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {

            var entities = await _employeeRepository.GetAllEmployees();
            var employees = new List<Employee>();
            foreach (var entity in entities)
            {
                var employee = new Employee(entity);
                employees.Add(employee);
            }
            return employees;
        }


        public async Task<List<string>> UpdateEmployee(Employee employee)
        {

            _logger.LogInformation("Loading employee details......");
            var entity = await _employeeRepository.GetEmployeeById(employee.Id);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("employee not found");
                return result;
            }

            var aggregate = new EmployeeAggregate(entity);
            aggregate.ValidateEmployee(employee);
            if (aggregate.ResultMessages.Count < 1)
            {
                //save employee 
                _logger.LogInformation("Saving employee details.....");
                aggregate.SaveEmployee(employee);
                await _employeeRepository.CreateEmployee (aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;

        }

        public async Task<List<string>> UpdateDepartment(Department department)
        {
            _logger.LogInformation("Loading department details......");
            var entity = await _departmentRepository.GetDepartmentById(department.Id);
            var result = new List<string>();

            if (entity == null)
            {
                result.Add("department not found");
                return result;
            }

            var aggregate = new DepartmentAggregate(entity);
            aggregate.ValidateDepartment(department);
            if (aggregate.ResultMessages.Count < 1)
            {
                //save department 
                _logger.LogInformation("Saving department details.....");
                aggregate.SaveDepartment(department);
                await _departmentRepository.CreateDepartment(aggregate.Entity);
            }
            else
            {
                result = aggregate.ResultMessages;
            }
            return result;

        }






    }
}
