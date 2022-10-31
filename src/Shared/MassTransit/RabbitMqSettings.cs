namespace Shared.MassTransit;

public class RabbitMqSettings
{
  public string Host { get; set; } = null!;
  public ushort Port { get; set; }
  public string VHost { get; set; } = null!;
  public string User { get; set; } = null!;
  public string Password { get; set; } = null!;
}