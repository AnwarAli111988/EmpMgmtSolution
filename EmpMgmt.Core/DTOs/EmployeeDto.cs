namespace EmpMgmt.Core.DTOs
{
    public record EmployeeDto
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? Position { get; init; }
    }
}
