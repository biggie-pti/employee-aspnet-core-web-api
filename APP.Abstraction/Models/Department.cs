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
   public  class Department 
    {
        public Department(DepartmentEntity entity )
        {
            this.Id = entity.Id;
            this.DepartmentName = entity.DepartmentName;

        }

        public Department()
        {
         
         
        }
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string DepartmentName { get; set; }
   
    }
}
