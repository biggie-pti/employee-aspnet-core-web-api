using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Abstraction.Entities
{
  public  interface IEntity
    {
        public bool IsDeleted { get; set; }
        public Guid Id { get; set; }
    }
}
