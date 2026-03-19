using Application;
using Infrastracture;
using Infrastracture.Identity;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
 
builder.Services.AddApplication(); 
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
 
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