namespace EmpMgmt.Core.Models
{
    public class ErrorResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
    }
}
