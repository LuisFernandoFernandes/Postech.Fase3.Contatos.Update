using Postech.Fase3.Contatos.Update.Infra.Messaging;

namespace Postech.Fase3.Contatos.Update.Service;

public class WkUpdateContato(
ILogger<WkUpdateContato> _logger,
RabbitMqConsumer _rabbitMqConsumer
) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        _rabbitMqConsumer.StartListeningAsync();
        return Task.CompletedTask;
    }
}
