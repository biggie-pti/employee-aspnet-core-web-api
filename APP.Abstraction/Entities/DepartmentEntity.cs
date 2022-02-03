using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Abstraction.Entities
{
   public  class DepartmentEntity :IEntity
    { 
        public string DepartmentName { get; set; }
        public bool IsDeleted { get; set ; }
        public Guid Id { get ; set ; }
    }
}
