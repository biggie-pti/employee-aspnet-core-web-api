using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Abstraction.Entities
{
    public class EmployeeEntity : IEntity
    {
   
        public string EmployeeName { get; set; }
        public string DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
        public string Department { get; set; }
        public bool IsDeleted { get ; set ; }
        public Guid Id { get ; set ; }
    }
}
