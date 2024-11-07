using EmpMgmt.Core.Entities;
using EmpMgmt.Core.IRepositories;
using EmpMgmt.Data.Data;

namespace EmpMgmt.Data.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
