
namespace EmpMgmt.Core.IRepositories
{
    public interface IUnitOfWork: IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
