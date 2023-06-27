using CRUDTest.Persistense;
using CRUDTest.Presentation.Middlewares;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CRUDTest.Presentation
{
    public static class ConfigureApplication
    {
        public static IApplicationBuilder ConfigurePresentationApplication(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseExceptionHandling();
            if (env.IsDevelopment())
                UseCustomSwagger(app);
            app.UseAuthentication();
            app.UseAuthorization();
            MigrateDatabase(app);

            return app;
        }

        private static void MigrateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                try
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    if (context.Database.IsSqlServer())
                        context.Database.Migrate();

                }
                catch (Exception ex)
                {
                    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating database.");
                    throw;
                }
            }
        }

        public static void UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/MyOpenAPI/swagger.json", "CRUD API");
                setupAction.DefaultModelExpandDepth(2);
                setupAction.DefaultModelRendering(ModelRendering.Model);
                setupAction.DocExpansion(DocExpansion.None);
                setupAction.EnableDeepLinking();
                setupAction.DisplayOperationId();
            });
        }
    }
}
