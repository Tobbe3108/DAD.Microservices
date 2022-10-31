using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.MassTransit
{
  public static class MassTransitExtensions
  {
    public static void AddMassTransit(this IServiceCollection services,
      IConfiguration configuration,
      Assembly? assembly = null)
    {
      services.AddMassTransit(x =>
      {
        x.UsingRabbitMq((context, cfg) =>
        {
          var rabbitMqSettings = configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>() ??
                                 throw new NullReferenceException("RabbitMqSettings not found");

          cfg.Host(rabbitMqSettings.Host,
            rabbitMqSettings.Port,
            rabbitMqSettings.VHost,
            h =>
            {
              h.Username(rabbitMqSettings.User);
              h.Password(rabbitMqSettings.Password);
            });

          cfg.ConfigureEndpoints(context);
        });

        if (assembly is not null) x.AddConsumers(assembly);
      });
    }
  }
}