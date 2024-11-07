using EmpMgmt.WebAPI;
using EmpMgmt.WebAPI.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAppDI(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}
else if (app.Environment.IsStaging())
{
    app.UseCors("AllowSpecificOrigins");
    app.UseRateLimiter();

}
else if (app.Environment.IsProduction())
{
    app.UseCors("AllowProductionOrigins");
    app.UseRateLimiter();
}

// Configure the HTTP request pipeline
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();
