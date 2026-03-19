using Application.Common;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastracture.Identity;
using Infrastracture.Persistence.EFCore;
using Infrastracture.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastracture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
         
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
 
        //services.AddAuthentication(options => {
        //    options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
        //    options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
        //}).AddBearerToken(IdentityConstants.BearerScheme);

        services.AddIdentityApiEndpoints<ApplicationUser>(options => {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
         
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        services.AddTransient<IEmailSender<ApplicationUser>, EmailSender>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
         
        services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));

        return services;
    }
}
