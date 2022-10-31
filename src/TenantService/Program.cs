using Autofac;
using Autofac.Extensions.DependencyInjection;
using Shared;
using Shared.MassTransit;

var builder = WebApplication.CreateBuilder(args);

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

logger.LogDebug("Ready");
await app.RunAsync();