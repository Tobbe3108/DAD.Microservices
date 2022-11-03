using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Autofac.DependencyInjection;

namespace Shared.Logging;

public static class LoggingExtensions
{
  public static void RegisterLogging(this ContainerBuilder containerBuilder,
    IConfiguration configuration)
  {
    var loggerConfiguration = new LoggerConfiguration()
      .ReadFrom.Configuration(configuration)
      .Enrich.WithProperty("SourceContext", string.Empty)
      .Enrich.WithProperty("Service", Assembly.GetEntryAssembly()?.GetName().Name ?? string.Empty)
      .Enrich.FromLogContext()
      .WriteTo.Async(sinkConfiguration =>
        sinkConfiguration.Console(
          outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}"));
    var seq = configuration.GetConnectionString("Seq");
    if (seq is not null)
    {
      loggerConfiguration.WriteTo.Async(sinkConfiguration => sinkConfiguration.Seq(seq));
    }

    containerBuilder.RegisterSerilog(loggerConfiguration);
  }
}