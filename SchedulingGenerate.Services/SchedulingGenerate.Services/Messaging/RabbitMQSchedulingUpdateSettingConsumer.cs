using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SchedulingGenerate.Services.Messages;
using SchedulingGenerate.Services.Models;
using SchedulingGenerate.Services.Services.IRepository;
using SchedulingGenerate.Services.Services.Repository;

namespace SchedulingGenerate.Services.Messaging;

public class RabbitMQSchedulingUpdateSettingConsumer: Microsoft.Extensions.Hosting.BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly SettingRepository _settingRepository;

    public RabbitMQSchedulingUpdateSettingConsumer(SettingRepository settingRepository)
    {
        //Todo: clean here
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest"
        };

        _settingRepository = settingRepository;

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "schedulingupdatesettingmessagequeue", false, false, false, arguments: null);
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            SchedulingSettingRequestDto schedulingSettingRequestDto = JsonConvert.DeserializeObject<SchedulingSettingRequestDto>(content);
            HandleCrudCourseMessage(schedulingSettingRequestDto).GetAwaiter().GetResult();

            _channel.BasicAck(ea.DeliveryTag, false);
        };
        _channel.BasicConsume("schedulingupdatesettingmessagequeue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task HandleCrudCourseMessage(SchedulingSettingRequestDto schedulingSettingRequestDto)
    {
        _settingRepository.Update(schedulingSettingRequestDto);
    }
}