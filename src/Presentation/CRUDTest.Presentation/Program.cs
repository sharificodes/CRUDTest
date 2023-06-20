using CRUDTest.Application;
using CRUDTest.Infrastructure;
using CRUDTest.Persistense;
using CRUDTest.Presentation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplicationServices()
                .RegisterInfrastructureServices()
                .RegisterPersistenceServices(builder.Configuration)
                .RegisterPresentationServices();

var app = builder.Build();

app.ConfigurePresentationApplication();