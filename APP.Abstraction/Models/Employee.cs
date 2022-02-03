using APP.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace APP.Abstraction.Models
{
    [DataContract]
    public class Employee
    {

        public Employee(EmployeeEntity entity)
        {
            this.Id = entity.Id;
            this.EmployeeName = entity.EmployeeName;
            this.DateOfJoining = entity.DateOfJoining;
            this.Department = entity.Department;
            this.PhotoFileName = entity.PhotoFileName;

        }
        public Employee()
        {

        }

        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string EmployeeName { get; set; }
        [DataMember]
        public string DateOfJoining { get; set; }
        [DataMember]
        public string PhotoFileName { get; set; }
        [DataMember]
        public string Department { get; set; }

  

    }
}
