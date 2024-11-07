using EmpMgmt.Core;
using EmpMgmt.Core.Models.KeyVaultsOptions;
using EmpMgmt.Data;
using EmpMgmt.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmpMgmt.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataDI()
                .AddServiceDI()
                .AddCoreDI(configuration);



            // Add rate limiting services with a fixed window policy
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("FixedWindowPolicy", limiterOptions =>
                {
                    limiterOptions.Window = TimeSpan.FromMinutes(1);
                    limiterOptions.PermitLimit = 100;
                    limiterOptions.QueueLimit = 10;
                    limiterOptions.AutoReplenishment = true;
                });
            });


            // Define all CORS policies
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });

                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("https://example.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });

                options.AddPolicy("AllowProductionOrigins", policy =>
                {
                    policy.WithOrigins("https://anotherdomain.com")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            if (jwtSettings == null)
            {
                throw new InvalidOperationException("JWT settings cannot be null. Please check the configuration.");
            }
            var key = Encoding.UTF8.GetBytes(jwtSettings.Key ?? string.Empty);
            bool AllowHttps = jwtSettings.RequireHttpsMetadata;
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                   .AddJwtBearer(x =>
                   {
                       x.RequireHttpsMetadata = AllowHttps;
                       x.SaveToken = true;
                       x.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = jwtSettings?.Issuer,
                           ValidAudience = jwtSettings?.Audience,
                           IssuerSigningKey = new SymmetricSecurityKey(key)
                       };
                   });



            return services;
        }
    }
}
