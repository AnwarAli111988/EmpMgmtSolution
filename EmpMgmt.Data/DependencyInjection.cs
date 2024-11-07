using EmpMgmt.Core.IRepositories;
using EmpMgmt.Core.Models.KeyVaultsOptions;
using EmpMgmt.Data.Data;
using EmpMgmt.Data.Repositories;
using EmpMgmt.Data.UnitOfWorkNamespace;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EmpMgmt.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataDI(this IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                options.UseSqlServer(provider.GetRequiredService<IOptions<DatabaseConnectionOptions>>().Value.DefaultConnection);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            return services;
        }
    }
}
