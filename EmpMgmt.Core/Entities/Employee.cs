using EmpMgmt.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmpMgmt.Core.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public EmployeeStatus Status { get; set; }
    }
}
