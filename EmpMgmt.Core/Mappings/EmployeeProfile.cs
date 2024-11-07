using AutoMapper;
using EmpMgmt.Core.DTOs;
using EmpMgmt.Core.Entities;

namespace EmpMgmt.Core.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
