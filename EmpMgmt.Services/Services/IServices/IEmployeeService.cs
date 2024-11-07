using EmpMgmt.Core.DTOs;
using EmpMgmt.Core.Models.FilterationModels;

namespace EmpMgmt.Services.Services.IServices
{
    public interface IEmployeeService
    {
        Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id);
        Task<PagedResult<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeFilter filter);
        Task AddEmployeeAsync(EmployeeDto employeeDto);
        Task UpdateEmployeeAsync(EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(Guid id);
    }
}
