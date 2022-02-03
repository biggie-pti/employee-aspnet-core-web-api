using APP.Abstraction.Entities;
using APP.Abstraction.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Infrastracture.Data
{
  public   class MongoContext
    {
       public readonly MongoClient _client;
      public readonly  IMongoDatabase mongoDatabase;

        public MongoContext()
        {
            _client = new MongoClient("mongodb://localhost");
            mongoDatabase = _client.GetDatabase("EmployDepartment");
        }

        public IMongoCollection<DepartmentEntity> Departments => mongoDatabase.GetCollection<DepartmentEntity>("Department");
        public IMongoCollection<EmployeeEntity> Employees => mongoDatabase.GetCollection<EmployeeEntity>("Employee");

    }
}
