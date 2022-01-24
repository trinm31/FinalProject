using System.Text;
using Email.Services.Messages;
using Email.Services.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Email.Services.Messaging;

public class RabbitMQPaymentConsumer: BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private const string ExchangeName= "DirectEmail_Exchange";
    private const string PaymentEmailUpdateQueueName = "EmailQueueName";
    private readonly EmailRepository _emailRepo;
    string queueName = "";
    public RabbitMQPaymentConsumer(EmailRepository emailRepo)
    {
        _emailRepo = emailRepo;
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct);
        _channel.QueueDeclare(PaymentEmailUpdateQueueName, false, false, false, null);
        _channel.QueueBind(PaymentEmailUpdateQueueName, ExchangeName, "PaymentEmail");
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            EmailMessage emailMessage = JsonConvert.DeserializeObject<EmailMessage>(content);
            HandleMessage(emailMessage).GetAwaiter().GetResult();

            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume(PaymentEmailUpdateQueueName, false, consumer);

        return Task.CompletedTask;
    }

    private async Task HandleMessage(EmailMessage emailMessage)
    {
        try
        {
            await _emailRepo.SendAndLogEmail(emailMessage);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}