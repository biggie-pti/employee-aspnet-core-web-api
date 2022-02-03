using APP.Abstraction.Entities;
using APP.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Abstraction.Repositories
{
   public  interface IDepartmentRepository
    {
        void UpdateDepartment(DepartmentEntity entity);
        void DeleteDepartment(Guid Id);
        Task<DepartmentEntity> GetDepartmentById(Guid Id);
        Task<List<DepartmentEntity>> GetAllDepartments();
        Task<Guid> CreateDepartment(DepartmentEntity entity);

    }
}
