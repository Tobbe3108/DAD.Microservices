using Autofac;
using Autofac.Extensions.DependencyInjection;
using Frontend.API.Features.Rental;
using Refit;
using Shared.Logging;
using Shared.MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options => { options.EnableAnnotations(); });
builder.Services.AddMassTransit(builder.Configuration);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces();
  containerBuilder.RegisterLogging(builder.Configuration);

  containerBuilder.Register(c =>
  {
    var rentalUri = builder.Configuration.GetValue<string>("RentalService");
    c.Resolve<ILogger<Program>>().LogDebug("RentalService: {RentalUri}", rentalUri);
    var rentalServiceApi = RestService.For<IRentalServiceApi>(rentalUri);
    return rentalServiceApi;
  }).As<IRentalServiceApi>();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

var services = app.Services.GetAutofacRoot();
var logger = services.Resolve<ILogger<Program>>();
logger.LogDebug("Ready");
await app.RunAsync();