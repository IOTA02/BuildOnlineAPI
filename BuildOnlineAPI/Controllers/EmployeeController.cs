using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BuildOnlineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationContext _dbContext;

        public EmployeesController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/api/GetAllPositions")]
        public IActionResult GetAllPositions()
        {
            var positions = _dbContext.Position.ToList();
            return Ok(positions);
        }

        [HttpGet("/api/GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            var employees = _dbContext.Employee.ToList();
            return Ok(employees);
        }

        [HttpGet("/api/GetEmployee/{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _dbContext.Employee.FirstOrDefault(u => u.Id == id);
            if (employee == null)
                return NotFound(new { message = "Employee not found" });
            return Ok(employee);
        }

        [HttpPost("/api/PostEmployee")]
        public IActionResult PostEmployee(Employee employee)
        {
            _dbContext.Employee.Add(employee);
            employee.Birthday = employee.Birthday.ToUniversalTime();
            _dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpPut("/api/PutEmployee/{id}")]
        public IActionResult PutEmployee(int id, Employee employeeData)
        {
            var employee = _dbContext.Employee.FirstOrDefault(u => u.Id == id);
            if (employee == null)
                return NotFound(new { message = "Employee not found" });
            employee.Firstname = employeeData.Firstname;
            employee.Surname = employeeData.Surname;
            employee.Birthday = employeeData.Birthday.ToUniversalTime();
            employee.IsActive = employeeData.IsActive;
            _dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete("/api/DeleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _dbContext.Employee.FirstOrDefault(u => u.Id == id);
            if (employee == null)
                return NotFound(new { message = "Employee not found" });
            _dbContext.Employee.Remove(employee);
            _dbContext.SaveChanges();
            return Ok(employee);
        }
    }
}