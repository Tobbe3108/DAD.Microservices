using Autofac;
using Autofac.Extensions.DependencyInjection;
using Frontend.API.Features.Rental;
using Refit;
using Shared;
using Shared.MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddMassTransit(builder.Configuration);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces();
  containerBuilder.RegisterLogging(builder.Environment.IsDevelopment());

  containerBuilder.Register(c =>
      RestService.For<IRentalServiceApi>(c.Resolve<IConfiguration>().GetValue<string>("RentalServiceApiUrl")))
    .As<IRentalServiceApi>();
});

var app = builder.Build();

var services = app.Services.GetAutofacRoot();
var logger = services.Resolve<ILogger<Program>>();

logger.LogDebug("Ready");
await app.RunAsync();