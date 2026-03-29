using Application;
using Infrastracture;
using Infrastracture.Identity;
using Scalar.AspNetCore;
using Serilog;
using TaskFlow.Middleware;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
   .MinimumLevel.Information()
   .WriteTo.Console()  
   .WriteTo.File("Logs/log-.txt",
       rollingInterval: RollingInterval.Day)  
   .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
 
builder.Services.AddApplication(); 
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();
 
app.MapIdentityApi<ApplicationUser>();
app.MapControllers();

app.Run();