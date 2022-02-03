using APP.Abstraction.Models;
using APP.Abstraction.Services;
using APP.API.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;

namespace APP.API.Controllers
{
    /// <summary>
    /// Employees Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IWebHostEnvironment _env;
        private readonly string _filePath;
        private readonly ILogger<EmployeesController> _logger;
        private readonly LoggingContext _loggingContext;

        /// <summary>
        /// Employees Controller constructor
        /// </summary>
        public EmployeesController(IApplicationService applicationService, IWebHostEnvironment env, string filePath, ILogger<EmployeesController>logger)
        {
            _applicationService = applicationService;
            _env = env;
            _filePath = filePath;
            _logger = logger;
            _loggingContext = new LoggingContext("localhost", " APP API", "Employees" +
                "Controller", "", 200);

        }


        /// <summary>
        /// Create department 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {

            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(employee);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode, data);
            var result = await _applicationService.CreateEmployee(employee);

            string folder = @"C:\Biggie\App\api\APPAPI\APPAPI\Filetxt\";
            //// Filename  
            string fileName = "example.txt";
            //// Fullpath. You can direct hardcode it if you like.  
            string fullPath = folder + fileName;

            //// Write array of strings to a file using WriteAllLines.  
            //// If the file does not exists, it will create a new file.  
            //// This method automatically opens the file, writes to it, and closes file  
            //File.WriteAllText (fullPath, result);

            // Write file using StreamWriter  
    
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine(result);
                writer.WriteLine("C Programming");
                writer.WriteLine("C++ Programming");
                writer.WriteLine("Back for Front-end");
                writer.WriteLine("Web programming");
            }



           

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
        /// Get all employees
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode);
            var result = await _applicationService.GetAllEmployees();
            return result;
        }

        /// <summary>
        /// Get employee by Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<Employee> GetEmployee(Guid Id)
        {
            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(Id);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode, data);
            return await _applicationService.GetEmployeeById(Id);
        }


        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Employee employee)
        {
            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(employee);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode, data);
            var result = await _applicationService.UpdateEmployee(employee);
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
        /// Delete employee
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {

            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            var data = JsonConvert.SerializeObject(Id);
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode, data);

            var result = await _applicationService.DeleteEmployee(Id);
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
        /// Save a photo from file to a local folder
        /// </summary>
        [Route("SaveFile")]
        [HttpPost]


        public JsonResult SavePhoto()
        {

            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode);

            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream= new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);



            }
            catch (Exception)
            {

                return new JsonResult("anonymous.pnp");
            }

        }

        /// <summary>
        /// Uploading file
        /// </summary>
        [Route("UploadFile")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("File uploading . Please wait ...........");
        }

        /// <summary>
        /// Downloading file to computer
        /// </summary>
        [Route("DownloadFile")]
        [HttpGet]
        public FileContentResult GetFile()
        {
            _loggingContext.ActionName = MethodBase.GetCurrentMethod().Name;
            _logger.LogInformation("Server Name: {0} Api Name : {1} Contoller Name : {2}, Action Name :{3} Status Code :{4} \n Data :{5}", _loggingContext.ServerName, _loggingContext.APIName, _loggingContext.ControllerName, _loggingContext.ActionName, _loggingContext.StatusCode);

            var data = System.IO.File.ReadAllBytes(_filePath);
            var result = new FileContentResult(data, "application/octet-stream")
            {
                FileDownloadName="File.csv"
            };
            return result;
            //alternatively
            //return File(System.IO.File.ReadAllBytes(_filePath), "application/octet-stream", "File.csv");

        }

        /// <summary>
        /// upload file
        /// </summary>
        [Route("Upload")]
        [HttpPost]
        public string Upload(IFormFile formFile)
        {
       
            return "File uploading . Please wait ...........";
        }

    }
}
