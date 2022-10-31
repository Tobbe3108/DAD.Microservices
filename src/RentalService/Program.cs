using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalService.Features.Database;
using Shared;
using Shared.MassTransit;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("Rental") ??
                 throw new NullReferenceException("Connection string not found");
builder.Services.AddDbContext<RentalDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddMassTransit(builder.Configuration, typeof(Program).Assembly);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces();
  containerBuilder.RegisterLogging(builder.Environment.IsDevelopment());
});

var app = builder.Build();

var services = app.Services.GetAutofacRoot();
var logger = services.Resolve<ILogger<Program>>();

app.MapGet("/",
  ([FromServices] RentalDbContext rentalDbContext) =>
    rentalDbContext.Rentals.ToListAsync());

app.MapGet("/{id:int}",
  (int id, [FromServices] RentalDbContext rentalDbContext) =>
    rentalDbContext.Rentals.FirstOrDefaultAsync(r => r.Id == id));

logger.LogDebug("Ready");
await app.RunAsync();