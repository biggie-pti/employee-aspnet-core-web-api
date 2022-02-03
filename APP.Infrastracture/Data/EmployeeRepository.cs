using APP.Abstraction.Entities;
using APP.Abstraction.Models;
using APP.Abstraction.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Infrastracture.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MongoContext _context;

        public EmployeeRepository()
        {
            _context = new MongoContext();
        }

        public async Task<Guid> CreateEmployee(EmployeeEntity entity)
        {
       
            FilterDefinition<EmployeeEntity> filter = Builders<EmployeeEntity>.Filter.Eq("_id", entity.Id);
            var result = await _context.Employees.FindAsync(filter);

            if (result.Any())
            {
                await _context.Employees.ReplaceOneAsync(filter, entity);
            }
            else
            {
                entity.Id = Guid.NewGuid();
                await _context.Employees.InsertOneAsync(entity);
            }

            return entity.Id;
        }


        public async Task<EmployeeEntity> GetEmployeeById(Guid Id)
        {
            var filter = Builders<EmployeeEntity>.Filter.Eq("_id", Id);
            var employee = (await _context.Employees.FindAsync(filter)).FirstOrDefault();
            return employee;
        }

        public async Task<List<EmployeeEntity>> GetAllEmployees()
        {

            FilterDefinition<EmployeeEntity> filter = Builders<EmployeeEntity>.Filter.Ne(s => s.IsDeleted, true);
            var employees = await _context.Employees.Find(filter).ToListAsync();
            return employees;
        }


        public void DeleteEmployee(Guid Id)
        {
            var filter = Builders<EmployeeEntity>.Filter.Eq("_id", Id);
            _context.Employees.DeleteOne(filter);

        }

      
        public void UpdateEmployee( EmployeeEntity entity)
        {

            var filter = Builders<EmployeeEntity>.Filter.Eq("_id", entity.Id);
            _context.Employees.ReplaceOne(filter, entity);
        }
    }
}
