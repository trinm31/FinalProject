using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Email.Services.Messages;
using Email.Services.Repository;
using Email.Services.Utility;
using MimeKit;
using MimeKit.Utils;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ContentType = MimeKit.ContentType;

namespace Email.Services.Messaging;

public class RabbitMQSendEmailConsumer: BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private const string ExchangeName= "DirectEmail_Exchange";
    private const string PaymentEmailUpdateQueueName = "EmailQueueName";
    private readonly EmailRepository _emailRepo;
    string queueName = "";
    private readonly SendMailService _sendMailService;

    public RabbitMQSendEmailConsumer(EmailRepository emailRepo, SendMailService sendMailService)
    {
        _emailRepo = emailRepo;
        _sendMailService = sendMailService;
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
            MailContent content = new MailContent();
                
            content.BodyBuilder = new BodyBuilder();
            content.To = emailMessage.Email;
            content.Subject = "ESM Check-in QR Code";
            
            var contentType = new ContentType ("image", "jpeg");
            var image = content.BodyBuilder.LinkedResources.Add(emailMessage.FileName,emailMessage.FileData,contentType);
            image.ContentId = MimeUtils.GenerateMessageId ();
                
            content.BodyBuilder.HtmlBody = QREmailTemplate.Template(image.ContentId, emailMessage.Email);

            await _sendMailService.SendMail(content);
            
            await _emailRepo.SendAndLogEmail(emailMessage);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}