using EmpMgmt.Core.Mappings;
using EmpMgmt.Core.Models.KeyVaultsOptions;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmpMgmt.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<DatabaseConnectionOptions>(configuration.GetSection(DatabaseConnectionOptions.SectionName));
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddAutoMapper(typeof(EmployeeProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());



            return services;
        }
    }
}
