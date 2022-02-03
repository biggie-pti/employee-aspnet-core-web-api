using APP.Abstraction.Models;
using APP.Abstraction.Services;
using APP.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace APP.API.Controllers
{
    /// <summary>
    /// Departments Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
  
    public class DepartmentsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly ILogger<DepartmentsController> _logger;
        private readonly LoggingContext _loggingContext;

        /// <summary>
        /// Departments Controller constructor
        /// </summary>
        public DepartmentsController(IApplicationService applicationService, ILogger<DepartmentsController> logger)
        {
            _applicationService = applicationService;
            _logger = logger;
            _loggingContext = new LoggingContext("localhost", " APP API", "Departments" +
                "Controller", "", 200);

        }

        /// <summary>
        /// Create department 
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Department), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post([FromBody] Department department)
        {
            //_logger.LogInformation("Creating department");

            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(department);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode, data);

            var result = await _applicationService.CreateDepartment(department);

            if (result.Count < 1)
            {
                return Ok(department);
            }
            else
            {
                return BadRequest(result);
            }
        }


        /// <summary>
        /// Get all departments 
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IEnumerable<Department>> GetDepartments()
        {
            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode);

            var result = await _applicationService.GetAllDepartments();
            return result;
        }
        /// <summary>
        /// Get department by Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet("{Id}")]

        public async Task<Department> GetDepartment(Guid Id)
        {

            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(Id);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode, data);

            return await _applicationService.GetDepartmentById(Id);
        }

        /// <summary>
        /// Update department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Department department)
        {

            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(department);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode, data);

            var result = await _applicationService.UpdateDepartment(department);
            if (result.Count < 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }

        }
        /// <summary>
        /// Delete department 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {

            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(Id);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode, data);

            var result = await _applicationService.DeleteDepartment(Id);
            if (result.Count < 1)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }


    }
}
