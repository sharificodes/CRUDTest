using CRUDTest.Application.Common.Interfaces;
using CRUDTest.Persistense.Extensions;
using CRUDTest.Persistense.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Persistense;

public static class ConfigureServices
{
    public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext"),
                 builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.ConfigureIdentity();
        services.ConfigureJWT(configuration);

        services.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>();

        return services;
    }
}
