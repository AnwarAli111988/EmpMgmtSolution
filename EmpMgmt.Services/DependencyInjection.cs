using EmpMgmt.Services.Services.IServices;
using EmpMgmt.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmpMgmt.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceDI(this IServiceCollection services)
        {

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
