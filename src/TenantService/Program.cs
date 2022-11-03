using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Logging;
using Shared.MassTransit;

var builder = WebApplication.CreateBuilder(args);

// var connection = builder.Configuration.GetConnectionString("DAD") ??
//                  throw new NullReferenceException("Connection string not found");
// builder.Services.AddDbContext<TenantDbContext>(options =>
//   options.UseSqlServer(connection.Replace("Database=master;", "Database=DAD_Tenant;")));

builder.Services.AddMassTransit(builder.Configuration, typeof(Program).Assembly);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces();
  containerBuilder.RegisterLogging(builder.Configuration);
});

var app = builder.Build();

var services = app.Services.GetAutofacRoot();
var logger = services.Resolve<ILogger<Program>>();

// await services.Resolve<TenantDbContext>().Database.MigrateAsync();

logger.LogDebug("Ready");
await app.RunAsync();