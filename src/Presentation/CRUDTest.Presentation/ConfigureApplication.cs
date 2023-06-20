using CRUDTest.Persistense;
using CRUDTest.Presentation.Middlewares;
using Microsoft.EntityFrameworkCore;

namespace CRUDTest.Presentation
{
    public static class ConfigureApplication
    {
        public static WebApplication ConfigurePresentationApplication(this WebApplication app)
        {
            app.UseExceptionHandling();

            // Perform automatic migrations
            MigrateDatabase(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            return app;
        }

        private static void MigrateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                try
                {
                    var context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
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
    }
}
