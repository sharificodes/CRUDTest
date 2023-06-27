using CRUDTest.Application;
using CRUDTest.Infrastructure;
using CRUDTest.Persistense;
using CRUDTest.Presentation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterApplicationServices()
                .RegisterInfrastructureServices()
                .RegisterPersistenceServices(builder.Configuration)
                .RegisterPresentationServices(builder.Logging, builder.Environment, builder.Configuration);

var app = builder.Build();
app.ConfigurePresentationApplication(app.Environment);
app.MapControllers();
app.Run();