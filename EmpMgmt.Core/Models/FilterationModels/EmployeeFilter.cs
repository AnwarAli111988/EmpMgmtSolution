namespace EmpMgmt.Core.Models.FilterationModels
{
    public class EmployeeFilter:PaginationModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
    }
}
