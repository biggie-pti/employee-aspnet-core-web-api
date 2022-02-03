using APP.Abstraction.Entities;
using APP.Abstraction.Models;
using APP.Abstraction.Repositories;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APP.API.graphQL
{
    /// <summary>
    /// Graphql query processing
    /// </summary>
    public class Query
    {
       
        /// <summary>
        /// Returns Departments based on user defined criteria
        /// </summary>
        public Task<List<DepartmentEntity>> GetDepartment([Service] IDepartmentRepository _departmentRepository)
        {
            return _departmentRepository.GetAllDepartments();

        }


        /// <summary>
        /// Get employees based on user defined criteria
        /// </summary>
        public Task<List<EmployeeEntity>> GetEmployee([Service] IEmployeeRepository _employeeRepository)
        {
            return _employeeRepository.GetAllEmployees();

        }


       

    }
}
