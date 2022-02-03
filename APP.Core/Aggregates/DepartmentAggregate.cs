using APP.Abstraction.Entities;
using APP.Abstraction.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Core.Aggregates
{
   public  class DepartmentAggregate : BaseAggregate<DepartmentEntity>
    {

    
        public DepartmentAggregate(DepartmentEntity entity) : base(entity)
        {
        }
            public void SaveDepartment(Department department)
            {
                SetDepartmentDetails(department);
            }

            private void SetDepartmentDetails(Department department)
            {
            Entity.Id = department.Id;
                Entity.DepartmentName = department.DepartmentName;
           
            }

        public void DeleteDepartment()
        {
            Entity.IsDeleted = true;
        }

        public void  ValidateDepartment(Department department)
        {

        }


    }
}
