using EmpMgmt.Core.DTOs;
using EmpMgmt.Core.Models.FilterationModels;
using EmpMgmt.Services.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EmpMgmt.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return employee != null ? Ok(employee) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees([FromQuery] EmployeeFilter filter)
        {
            var employees = await _employeeService.GetFilteredEmployeesAsync(filter);
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            await _employeeService.AddEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeDto.Id }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto employeeDto)
        {
            if (id != employeeDto.Id)
                return BadRequest("Employee ID mismatch");

            await _employeeService.UpdateEmployeeAsync(employeeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
