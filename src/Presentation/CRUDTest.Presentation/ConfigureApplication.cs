using CRUDTest.Persistense;
using Microsoft.EntityFrameworkCore;

namespace CRUDTest.Presentation
{
    public static class ConfigureApplication
    {
        public static WebApplication ConfigurePresentationApplication(this WebApplication app)
        {
            // Perform automatic migrations
            using (var serviceScope = app.ConfigurePresentationApplication().Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            return app;
        }
    }
}
