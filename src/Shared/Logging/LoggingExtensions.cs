using Autofac;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Autofac.DependencyInjection;

namespace Shared;

public static class LoggingExtensions
{
  public static void RegisterLogging(this ContainerBuilder containerBuilder, bool isDevelopment)
  {
    var level = isDevelopment
      ? LogEventLevel.Verbose
      : LogEventLevel.Warning;
    containerBuilder.RegisterSerilog(new LoggerConfiguration()
      .MinimumLevel.Is(level)
      .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
      .Enrich.WithProperty("SourceContext", string.Empty)
      .Enrich.FromLogContext()
      .WriteTo.Async(sinkConfiguration =>
        sinkConfiguration.Console(
          outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}")));
  }
}