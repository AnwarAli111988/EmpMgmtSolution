using EmpMgmt.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpMgmt.Data.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<Employee> tblEmployee { get; set; }
    }
}
