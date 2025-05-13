using Employee.DAL.Services;
using Employee.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Employee.DAL.Models.DTO;
namespace EmployeeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly ILoggerService _logger;

        // Inject both repository and logger via constructor
        public EmployeeController(IEmployeeRepository repo, ILoggerService logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var emp = _repo.GetById(id);
            _logger.Log($"Fetched employee with ID {id}");
            return emp == null ? NotFound() : Ok(emp);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _repo.GetAll();
            _logger.Log("Fetched all employees");
            return Ok(employees);
        }

        [HttpPost]
        public IActionResult Create(AddEmployeeDTO emp)
        {
            _repo.Create(emp);
            _logger.Log($"Created employee {emp.EmployeeName}");
            return Ok($"Employee {emp.EmployeeName} added");
        }

        [HttpPut]
        public IActionResult Update(EmployeeModel emp)
        {
            _repo.Update(emp);
            _logger.Log($"Updated employee ID {emp.EmployeeId}");
            return Ok($"Employee with {emp.EmployeeId} updated");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.SoftDelete(id);
            _logger.Log($"Soft deleted employee ID {id}");
            return Ok($"Employee {id} soft deleted ");
        }
    }
}
