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
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly MongoContext _context;

        public DepartmentRepository()
        {
            _context = new MongoContext();
        }

        public async Task<Guid> CreateDepartment(DepartmentEntity entity)
        {
            

            FilterDefinition<DepartmentEntity> filter = Builders<DepartmentEntity>.Filter.Eq("_id", entity.Id);
            var result = await _context.Departments.FindAsync(filter);

            if (result.Any())
            {
                await _context.Departments.ReplaceOneAsync(filter, entity);
            }
            else
            {
                entity.Id = Guid.NewGuid();
                await _context.Departments.InsertOneAsync(entity);
            }

            return entity.Id;
        }

        public async Task<Abstraction.Entities.DepartmentEntity> GetDepartmentById(Guid Id)
        {
            var filter = Builders<Abstraction.Entities.DepartmentEntity>.Filter.Eq("_id", Id);
            var department = (await _context.Departments.FindAsync(filter)).FirstOrDefault();
            return department;
        }

        public async Task<List<Abstraction.Entities.DepartmentEntity>> GetAllDepartments()
        {
            FilterDefinition<Abstraction.Entities.DepartmentEntity> filter = Builders<Abstraction.Entities.DepartmentEntity>.Filter.Ne(s => s.IsDeleted, true);
            var departments = await _context.Departments.Find(filter).ToListAsync();
            return departments;
        }

        public void DeleteDepartment(Guid Id)
        {
            var filter = Builders<Abstraction.Entities.DepartmentEntity>.Filter.Eq("_id", Id);
            _context.Departments.DeleteOne(filter);
    
        }

     

   
        public void UpdateDepartment(Abstraction.Entities.DepartmentEntity entity)
        {
            var filter = Builders<DepartmentEntity>.Filter.Eq("_id", entity.Id);
            _context.Departments.ReplaceOne(filter,entity);
        }

       
    }
}
