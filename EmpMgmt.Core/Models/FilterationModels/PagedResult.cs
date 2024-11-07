namespace EmpMgmt.Core.Models.FilterationModels
{
    public class PagedResult<T>
    {
        public int TotalRecords { get; set; }
        public IEnumerable<T>? Data { get; set; }
    }
}
