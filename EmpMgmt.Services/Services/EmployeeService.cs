using AutoMapper;
using EmpMgmt.Core.DTOs;
using EmpMgmt.Core.Entities;
using EmpMgmt.Core.IRepositories;
using EmpMgmt.Core.Models.FilterationModels;
using EmpMgmt.Services.Services.IServices;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EmpMgmt.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<EmployeeDto> _validator;

        public EmployeeService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<EmployeeDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            return employee != null ? _mapper.Map<EmployeeDto>(employee) : null;
        }

        public async Task<PagedResult<EmployeeDto>> GetFilteredEmployeesAsync(EmployeeFilter filter)
        {
            var query = _unitOfWork.EmployeeRepository.GetQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                query = query.Where(e => e.FirstName != null && e.FirstName.Contains(filter.FirstName));
            }
            if (!string.IsNullOrEmpty(filter.LastName))
            {
                query = query.Where(e => e.LastName != null && e.LastName.Contains(filter.LastName));
            }
            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(e => e.Email != null && e.Email.Contains(filter.Email));
            }
            if (!string.IsNullOrEmpty(filter.Position))
            {
                query = query.Where(e => e.Position != null && e.Position.Contains(filter.Position));
            }

            // Apply pagination
            var totalRecords = await query.CountAsync();
            var employees = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResult<EmployeeDto>
            {
                TotalRecords = totalRecords,
                Data = _mapper.Map<IEnumerable<EmployeeDto>>(employees)
            };
        }

        public async Task AddEmployeeAsync(EmployeeDto employeeDto)
        {
            await ValidateEmployeeAsync(employeeDto);

            var employee = _mapper.Map<Employee>(employeeDto);
            await _unitOfWork.EmployeeRepository.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employeeDto)
        {
            await ValidateEmployeeAsync(employeeDto);

            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(employeeDto.Id);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            _mapper.Map(employeeDto, employee);
            _unitOfWork.EmployeeRepository.Update(employee);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(id);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            _unitOfWork.EmployeeRepository.Remove(employee);
            await _unitOfWork.SaveChangesAsync();

        }

        private async Task ValidateEmployeeAsync(EmployeeDto employeeDto)
        {
            var validationResult = await _validator.ValidateAsync(employeeDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                throw new ValidationException(string.Join("; ", errors));
            }
        }

    }

}
