using Microsoft.OpenApi.Models;

namespace CRUDTest.Presentation
{
    public static class ConfigureServices
    {
        public static IServiceCollection RegisterPresentationServices(this IServiceCollection services, ILoggingBuilder logging, IHostEnvironment env, IConfiguration configuration)
        {
            ConfigureLogging(logging, env, configuration);
            services.AddHttpContextAccessor();
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            AddCustomSwagger(services);
            return services;
        }

        private static void ConfigureLogging(ILoggingBuilder logging, IHostEnvironment env, IConfiguration configuration)
        {
            logging.ClearProviders();
            logging.AddDebug();
            if (env.IsDevelopment())
                logging.AddConsole();
            logging.AddConfiguration(configuration.GetSection("Logging"));
        }

        public static void AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("MyOpenAPI", new OpenApiInfo());

                var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => setupAction.IncludeXmlComments(xmlFile));

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                });

                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                                {
                                    Type = ReferenceType
                                        .SecurityScheme,
                                    Id = "Bearer",
                                },
                        },
                        new List<string>()
                    },
                });
            });
        }
    }
}
