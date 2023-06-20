using CRUDTest.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDTest.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

            return services;
        }
    }
}
